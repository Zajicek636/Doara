import {Component, OnInit, ViewChild} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {TableSettings} from '../../shared/table/table/table.settings';
import {SkladyPohybyPolozkyDataService} from './data/sklady-pohyby-polozky-data.service';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {BreadcrumbService, IBreadCrumb} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {
  ADD_NEW_ITEMS_TO_CONTAINER,
  MOVEMENT_COLUMNS,
  MovementCategory,
  MovementDto
} from './data/sklady-pohyby-polozky.interfaces';
import {PolozkaKontejneruDataService} from '../polozka-kontejneru/data/polozka-kontejneru-data.service';
import {ContainerItemDto} from '../polozka-kontejneru/data/polozka-kontejneru.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';

@Component({
  selector: 'app-sklady-editace',
  imports: [SharedModule],
  templateUrl: './sklady-pohyby-polozky.component.html',
  styleUrl: './sklady-pohyby-polozky.component.scss'
})
export class SkladyPohybyPolozkyComponent extends BaseContentComponent<MovementDto, SkladyPohybyPolozkyDataService> implements OnInit {
  dataSource: MovementDto[] = [];
  loaded: boolean = false;
  @ViewChild(DynamicTableComponent) tableComponent!: DynamicTableComponent<MovementDto>;
  override ngOnInit() {
    super.ngOnInit();
    this.tableSettings = {
      cacheEntityType: "entity",
      columns: MOVEMENT_COLUMNS,
      clickable: true,
      expandable: false,
      pageSizeOptions: [10, 30, 50, 100],
      defaultPageSize: 10,
      showPaginator: false
    };
    this.loadData()
  }

  protected override buildToolbarButtons(): ToolbarButton<MovementDto>[] {
    return [
      {
        id: 'add',
        text: 'Nový pohyb',
        icon: 'add',
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => this.onAdd()
      },
      {
        id: 'delete',
        text: 'Smazat',
        icon: 'delete',
        class: 'btn-danger',
        disabled: !this.chosenElement || this.chosenElement.movementCategory !== MovementCategory.Unused,
        visible: true,
        action: () => this.onDelete()
      }
    ];
  }

  constructor(
    protected override dataService: SkladyPohybyPolozkyDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute,
    private polozkaKontejneruDataService: PolozkaKontejneruDataService,
  ) {
    super(route, router,breadcrumbService, dialogService, dataService);

  }

  public async handleDoubleClick(event: any) {

  }

  async loadData(): Promise<void> {
    this.dataSource = (await this.polozkaKontejneruDataService.get(this.entityId!)).movements!;
    this.dataSource = this.dataSource.reverse()
    this.loaded = true
  }

  async onAdd(): Promise<void> {
    const res = await this.dialogService.form({
      headerIcon: BaseMaterialIcons.PLUS,
      title: `Nový pohyb`,
      sections: [
        {
          sectionId: "main_section",
          headerIcon: BaseMaterialIcons.ASSIGNMENT,
          fields: ADD_NEW_ITEMS_TO_CONTAINER,
          sectionTitle: "Pohyb"
        }],
      type: DialogType.SUCCESS
    })

    if(!res) return;
    try {
      const pohyb: MovementDto = {quantity: res['main_section'].data.quantity,}
      const response = await this.dataService.postBySuffix(pohyb, "AddStock", this.entityId!);
      this.tableComponent.dataSource.data = [...response.movements].reverse();
    } catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e.error.error.message,
        dialogType: DialogType.ERROR,
      })
    }
  }

  async onDelete(): Promise<void> {
    if(!this.chosenElement) return;
    const res = await this.dialogService.confirmAsync({
      title: "Smazání",
      message: "Chcete opravdu smazat záznam?",
      dialogType: DialogType.ALERT,
      cancelButton: "Zrušit akci",
      confirmButton: "Potvrdit"
    })
    if(!res) return
    try {
      await this.dataService.postBySuffix({} as MovementDto,"RemoveMovement",this.entityId!, this.chosenElement?.id);
      this.removeItemFromTable()
      this.chosenElement = undefined
    } catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e.error.error.message,
        dialogType: DialogType.ERROR,
      })
    }
  }

  private removeItemFromTable(): void {
    const data = this.tableComponent.dataSource.data;
    const index = data.findIndex(el => el.id === this.chosenElement?.id);
    if (index !== -1) {
      data.splice(index, 1);
      this.tableComponent.dataSource.data = [...data];
    }
  }

  clickedElement(element: MovementDto) {
    console.log("clickedElement", element);
    this.chosenElement = element
  }
}
