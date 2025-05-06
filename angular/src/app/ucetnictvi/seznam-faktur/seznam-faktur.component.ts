import {Component, OnInit, ViewChild} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {BreadcrumbService, IBreadCrumb} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router, RouterOutlet} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {INVOICE_COLUMNS} from './data/seznam-faktur.interfaces';
import {SeznamFakurDataService} from './data/seznam-fakur-data.service';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {ColumnSetting} from '../../shared/table/table/table.settings';
import {PagedList} from '../../shared/crud/base-crud-service';
import {InvoiceDto} from '../nova-faktura/data/nova-faktura.interfaces';

@Component({
  selector: 'app-seznam-faktur',
  imports: [SharedModule],
  templateUrl: './seznam-faktur.component.html',
  styleUrl: './seznam-faktur.component.scss'
})
export class SeznamFakturComponent extends BaseContentComponent<InvoiceDto, SeznamFakurDataService> implements OnInit {
  dataSource: InvoiceDto[] = [];

  @ViewChild(DynamicTableComponent) tableComponent!: DynamicTableComponent<InvoiceDto>;
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
    this.tableSettings = {
      cacheEntityType: "entity",
      columns: INVOICE_COLUMNS,
      clickable: true,
      expandable: false,
      pageSizeOptions: [10, 30, 50, 100],
      defaultPageSize: 10,
    };
    this.loadData()
  }

  protected override buildToolbarButtons(): ToolbarButton<InvoiceDto>[] {
    return [
      {
        id: 'add',
        text: 'Nová faktura',
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
    this.router.navigate([this.basePath,'faktura'], {state: { previousBreadcrumbs: prev }});
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

  clickedElement(element: InvoiceDto) {
    console.log("clickedElement", element);
    this.chosenElement = element
  }

}
