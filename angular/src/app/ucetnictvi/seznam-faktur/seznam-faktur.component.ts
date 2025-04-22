import {Component, OnInit} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Route, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {SeznamFakturDto} from './data/seznam-faktur.interfaces';
import {SeznamFakurDataService} from './data/seznam-fakur-data.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
@Component({
  selector: 'app-seznam-faktur',
  imports: [SharedModule],
  templateUrl: './seznam-faktur.component.html',
  styleUrl: './seznam-faktur.component.scss'
})
export class SeznamFakturComponent extends BaseContentComponent<SeznamFakturDto, SeznamFakurDataService> implements OnInit {
  constructor(
    protected override dataService: SeznamFakurDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute
  ) {
    super(route, router,breadcrumbService, dialogService, dataService);

  }

  override get toolbarButtons(): any[] {
    return [
      {
        id: 'add',
        text: 'Přidat',
        icon: 'add',
        class: 'btn-primary',
        action: () => this.onAdd()
      },
      {
        id: 'edit',
        text: 'Upravit',
        icon: 'edit',
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        action: () => this.onEdit(this.chosenElement!)
      },
      {
        id: 'delete',
        text: 'Smazat',
        icon: 'delete',
        class: 'btn-danger',
        disabled: !this.chosenElement,
        action: () => this.onDelete(this.chosenElement!)
      }
    ];
  }

  ngOnInit() {
    this.tableSettings = {
      cacheEntityType: "entity",
      displayedColumns: ['id', "subjektname","subjektIco"],
      clickable: true,
      expandable: false,
      pageSizeOptions: [10, 30, 50, 100],
      defaultPageSize: 10,
      extraQueryParams: { active: true }
    };
  }
  public async handleDoubleClick(event: any) {
  }

  loadData(): void {
  }

  onAdd(): void {

  }

  onEdit(el: SeznamFakturDto): void {
    console.log("On edit", el)

  }

  onDelete(item: any): void {
    console.log("On delete", item);
  }

  clickedElement(element: SeznamFakturDto) {
    console.log("clickedElement", element);
    this.chosenElement = element
  }

}
