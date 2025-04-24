import {Component, OnInit, ViewChild} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {BreadcrumbService, IBreadCrumb} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router, RouterOutlet} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {SeznamFakturDto} from './data/seznam-faktur.interfaces';
import {SeznamFakurDataService} from './data/seznam-fakur-data.service';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {ColumnSetting} from '../../shared/table/table/table.settings';

@Component({
  selector: 'app-seznam-faktur',
  imports: [SharedModule],
  templateUrl: './seznam-faktur.component.html',
  styleUrl: './seznam-faktur.component.scss'
})
export class SeznamFakturComponent extends BaseContentComponent<SeznamFakturDto, SeznamFakurDataService> implements OnInit {

  @ViewChild(DynamicTableComponent) tableComponent!: DynamicTableComponent<SeznamFakturDto>;
  constructor(
    protected override dataService: SeznamFakurDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute
  ) {
    super(route, router,breadcrumbService, dialogService, dataService);

  }

  override ngOnInit() {
    super.ngOnInit();

    const columnSettings: ColumnSetting<SeznamFakturDto>[] = [
      { key: 'id', header: 'ID', valueGetter: r => r.id  },
      { key: 'subjektname', header: 'Název subjektu', valueGetter: r => r.subjektname },
    ];

    this.tableSettings = {
      cacheEntityType: "entity",
      columns: columnSettings,
      clickable: true,
      expandable: false,
      pageSizeOptions: [10, 30, 50, 100],
      defaultPageSize: 10,
      extraQueryParams: { active: true }
    };
  }

  protected override buildToolbarButtons(): ToolbarButton<SeznamFakturDto>[] {
    return [
      {
        id: 'add',
        text: 'Přidat',
        icon: 'add',
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => this.onAdd()
      },
      {
        id: 'edit',
        text: 'Upravit',
        icon: 'edit',
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.onEdit()
      },
      {
        id: 'delete',
        text: 'Smazat',
        icon: 'delete',
        class: 'btn-danger',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.onDelete()
      }
    ];
  }

  public async handleDoubleClick(event: any) {
  }

  loadData(): void {
  }

  onAdd(): void {
    const prev: IBreadCrumb[] = this.breadcrumbService.breadcrumbsValue;
    this.router.navigate([this.basePath,'nova-faktura'], {state: { previousBreadcrumbs: prev }});
  }

  onEdit(): void {
    console.log("On edit")
  }

  async onDelete(): Promise<void> {
    const res = await this.dialogService.confirmAsync({
      title: "Smazání",
      message: "Chcete opravdu smazat záznam?",
      dialogType: DialogType.ALERT,
      cancelButton: "Zrušit akci",
      confirmButton: "Potvrdit"
    })
    if(!res) return
    try {
      //todo doimplementovat volani api
      //await this.dataService.delete(this.chosenElement?.id!)
      //await this.tableComponent.loadData()
      this.removeItemFromTable()
      this.chosenElement = undefined
    } catch (e) {
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

  clickedElement(element: SeznamFakturDto) {
    console.log("clickedElement", element);
    this.chosenElement = element
  }

}
