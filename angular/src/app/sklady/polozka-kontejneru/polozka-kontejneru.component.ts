import {Component, OnInit, ViewChild} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {FormGroup} from '@angular/forms';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {PolozkaKontejneruDataService} from './data/polozka-kontejneru-data.service';
import {
  CONTAINER_ITEM_CREATE_FIELDS,
  CONTAINER_ITEM_FIELDS, ContainerItemCreateEditDto,
  ContainerItemDto
} from "./data/polozka-kontejneru.interfaces";
import {DialogType, DynamicDialogResult} from "../../shared/dialog/dialog.interfaces";
import {CustomValidator} from "../../shared/forms/form.interfaces";
import {populateDefaults} from "../../shared/forms/form-field.utils";
import {LoadingService} from '../../shared/loading/loading.service';

@Component({
  selector: 'app-polozka-kontejneru',
  imports: [SharedModule],
  templateUrl: './polozka-kontejneru.component.html',
  styleUrl: './polozka-kontejneru.component.scss'
})
export class PolozkaKontejneruComponent extends BaseContentComponent<ContainerItemDto, PolozkaKontejneruDataService> implements OnInit  {

  @ViewChild(DynamicTableComponent) tableComponent!: DynamicTableComponent<ContainerItemDto>;

  constructor(
    protected override dataService: PolozkaKontejneruDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute,
    protected loadingService: LoadingService,
  ) {
    super(route, router,breadcrumbService, dialogService, dataService);

  }

  form!: FormGroup;

  override ngOnInit() {
    super.ngOnInit();
    this.entityId = this.route.snapshot.paramMap.get('id');

    this.tableSettings = {
      cacheEntityType: "subjektyTableEntity",
      formFields: [...CONTAINER_ITEM_FIELDS],
      clickable: true,
      expandable: false,
      pageSizeOptions: [10, 30, 50, 100],
      defaultPageSize: 10,
      showPaginator: true,
      extraQueryParams: {ContainerId: this.entityId}
    };
  }

  protected buildToolbarButtons(): ToolbarButton[] {
    return [
      {
        id: 'add',
        text: 'Přidat položku',
        icon: BaseMaterialIcons.PLUS,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => this.addNewItemContainer()
      },
      {
        id: 'edit',
        text: 'Upravit',
        icon: BaseMaterialIcons.EDIT,
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.editItemContainer()
      },
      {
        id: 'pohyby',
        text: 'Pohyby',
        icon: BaseMaterialIcons.GRAPH_INCREASE,
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.navigateToPohyby()
      },
      {
        id: 'delete',
        text: 'Smazat',
        icon: BaseMaterialIcons.DELETE,
        class: 'btn-danger',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.deleteItemContainer()
      }
    ];
  }

  async addNewItemContainer() {
    const requiredFields = CONTAINER_ITEM_CREATE_FIELDS.filter(field =>
        field.validator?.some(v => v.validator === CustomValidator.REQUIRED)
    );

    const optionalFields = CONTAINER_ITEM_CREATE_FIELDS.filter(field =>
        !field.validator?.some(v => v.validator === CustomValidator.REQUIRED)
    );

    const dialogResult = await this.dialogService.form({
      headerIcon: BaseMaterialIcons.ADD_CONTAINER,
      title: "Přidat novou položku do kontejneru",
      sections: [
        {
          sectionId: "main_section",
          headerIcon: BaseMaterialIcons.ASSIGNMENT,
          fields: requiredFields,
          sectionTitle: "Hlavní informace"
        },
        {
          sectionId: "second_section",
          headerIcon: BaseMaterialIcons.LIST_ICON,
          fields: optionalFields,
          sectionTitle: "Doplňující informace"
        }
      ],
      type: DialogType.SUCCESS
    });

    if (!dialogResult) return;
    const newObj = this.mapToDto(dialogResult);
    const res = await this.dataService.post('', newObj)
    this.tableComponent.dataSource.data = [
      ...this.tableComponent.dataSource.data,
      res
    ]
  }

  async deleteItemContainer() { if(!this.chosenElement) return;
    const res = await this.dialogService.confirmAsync({
      title: "Potvrzení smazání",
      icon: BaseMaterialIcons.DELETE,
      message: `Opravdu chcete odebrat element: <strong>${this.chosenElement.name}</strong> ?`,
      dialogType: DialogType.ALERT,
      cancelButton: "Ne",
      confirmButton: "Ano"
    })
    if(!res) return;

    try {
      await this.dataService.delete(this.chosenElement.id!)
      const data = this.tableComponent.dataSource.data;
      const index = data.findIndex(el => el.id === this.chosenElement?.id);
      if (index !== -1) {
        data.splice(index, 1);
        this.tableComponent.dataSource.data = [...data];
      }

    } catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e.errors,
        dialogType: DialogType.ERROR
      })
    }
  }

  async editItemContainer() {
    let requiredFields = CONTAINER_ITEM_CREATE_FIELDS.filter(field =>
        field.validator?.some(v => v.validator === CustomValidator.REQUIRED)
    );

    let optionalFields = CONTAINER_ITEM_CREATE_FIELDS.filter(field =>
        !field.validator?.some(v => v.validator === CustomValidator.REQUIRED)
    );

    requiredFields = populateDefaults(requiredFields, this.chosenElement);
    optionalFields = populateDefaults(optionalFields, this.chosenElement);

    const dialogResult = await this.dialogService.form({
      headerIcon: BaseMaterialIcons.PLUS,
      title: "Editovat položku v kontejneru",
      sections: [
        {
          sectionId: "main_section",
          headerIcon: BaseMaterialIcons.ASSIGNMENT,
          fields: requiredFields,
          sectionTitle: "Hlavní informace"
        },
        {
          sectionId: "second_section",
          headerIcon: BaseMaterialIcons.LIST_ICON,
          fields: optionalFields,
          sectionTitle: "Doplňující informace"
        }
      ],
      type: DialogType.SUCCESS
    });

    const newObj = this.mapToDto(dialogResult);
    const res = await this.dataService.put(this.chosenElement?.id!, newObj)
    this.tableComponent.dataSource.data = this.tableComponent.dataSource.data.map(item =>
      item.id === this.chosenElement?.id ? res : item
    );
  }

  mapToDto(dialogResult: DynamicDialogResult): ContainerItemCreateEditDto {
    const main = dialogResult["main_section"]?.data || {};
    const second = dialogResult["second_section"]?.data || {};

    return {
      id: this.chosenElement?.id! ?? '',
      name: main.name,
      description: main.description ?? '',
      quantityType: main.quantityType.value,
      realPrice: main.realPrice,
      markup: main.markup || null,
      markupRate: second.markupRate || null,
      discount: second.discount || null,
      discountRate: second.discountRate || null,
      purchaseUrl: second.purchaseUrl || null,
      containerId: this.entityId!
    };
  }

  navigateToPohyby() {
    this.router.navigate([this.basePath,'pohyby-polozky', this.chosenElement?.id], {state: { previousBreadcrumbs: this.breadcrumbService.breadcrumbsValue }});
  }

  clickedElement(item: ContainerItemDto) {
    this.chosenElement = item;
  }

  async handleDoubleClick(item: ContainerItemDto) {
    this.chosenElement = item;
    this.navigateToPohyby();
  }
}
