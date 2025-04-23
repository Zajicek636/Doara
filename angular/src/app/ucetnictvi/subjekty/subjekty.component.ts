import {Component, OnInit} from '@angular/core';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {SubjektyDto} from './data/subjekty.interfaces';
import {SubjektyDataService} from './data/subjekty-data.service';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {SharedModule} from '../../shared/shared.module';

@Component({
  selector: 'app-subjekty',
  imports: [
    SharedModule
  ],
  templateUrl: './subjekty.component.html',
  styleUrl: './subjekty.component.scss'
})
export class SubjektyComponent  extends BaseContentComponent<SubjektyDto, SubjektyDataService> implements OnInit {
  constructor(
    protected override dataService: SubjektyDataService,
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
      displayedColumns: ["jmeno","prijmeni","ico"],
      clickable: true,
      expandable: false,
      pageSizeOptions: [10, 30, 50, 100],
      defaultPageSize: 10,
      extraQueryParams: { active: true }
    };
  }

  protected buildToolbarButtons(): ToolbarButton[] {
    return [
      {
        id: 'add',
        text: 'Přidat subjekt',
        icon: BaseMaterialIcons.NEW_QUOTE,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => {}
      },
      {
        id: 'edit',
        text: 'Upravit subjekt',
        icon: 'edit',
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => {}
      },
      {
        id: 'delete',
        text: 'Smazat subjekt',
        icon: 'delete',
        class: 'btn-danger',
        disabled: !this.chosenElement,
        visible: true,
        action: () => {}
      }
    ];
  }

  public handleDoubleClick(row: SubjektyDto) {
  }

  public clickedElement(row: SubjektyDto) {
    console.log('clickedElement', row);
    this.chosenElement = row;
  }
}
