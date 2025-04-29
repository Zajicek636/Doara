import {Component, OnInit, ViewChild} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {
  SUBJEKT_ADDRESS_FIELDS,
  SUBJEKT_BASE_FIELDS,
  SubjektDetailDto
} from '../../ucetnictvi/subjekty/data/subjekty.interfaces';
import {SubjektyDataService} from '../../ucetnictvi/subjekty/data/subjekty-data.service';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {FormGroup} from '@angular/forms';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {PolozkaKontejneruDataService} from './data/polozka-kontejneru-data.service';

@Component({
  selector: 'app-polozka-kontejneru',
  imports: [SharedModule],
  templateUrl: './polozka-kontejneru.component.html',
  styleUrl: './polozka-kontejneru.component.scss'
})
export class PolozkaKontejneruComponent extends BaseContentComponent<any, any> implements OnInit  {
  @ViewChild(DynamicTableComponent) tableComponent!: DynamicTableComponent<any>;

  constructor(
    protected override dataService: PolozkaKontejneruDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute
  ) {
    super(route, router,breadcrumbService, dialogService, dataService);

  }

  form!: FormGroup;

  override ngOnInit() {
    super.ngOnInit();
    this.tableSettings = {
      cacheEntityType: "subjektyTableEntity",
      formFields: [...SUBJEKT_BASE_FIELDS, ...SUBJEKT_ADDRESS_FIELDS],
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
        text: 'Přidat položku',
        icon: BaseMaterialIcons.ADD_PERSON,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => {}
      },

      {
        id: 'edit',
        text: 'Upravit',
        icon: BaseMaterialIcons.EDIT_PERSON,
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => {}
      },
      {
        id: 'delete',
        text: 'Smazat',
        icon: BaseMaterialIcons.REMOVE_PERSON,
        class: 'btn-danger',
        disabled: !this.chosenElement,
        visible: true,
        action: () => {}
      }
    ];
  }
}
