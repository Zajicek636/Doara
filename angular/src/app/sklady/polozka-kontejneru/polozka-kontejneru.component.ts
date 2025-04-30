import {Component, OnInit, ViewChild} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {PolozkaKontejneruDataService} from './data/polozka-kontejneru-data.service';
import {
  CONTAINER_ITEM_CREATE_FIELDS,
  CONTAINER_ITEM_FIELDS, ContainerItemCreateInputDto,
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
  entityId: string|null = null;

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
      extraQueryParams: {id: this.entityId},
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
    const res = await this.dataService.post<ContainerItemDto>('', newObj)
    //this.tableComponent.dataSource.data.push(res);
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
      //this.dialogService.delete(this.chosenElement.id)
      const data = this.tableComponent.dataSource.data;
      const index = data.findIndex(el => el.id === this.chosenElement?.id);
      if (index !== -1) {
        data.splice(index, 1);
        this.tableComponent.dataSource.data = [...data];
      }

    } catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e,
        dialogType: DialogType.ERROR
      })
    }
  }

  async editItemContainer() {
    let requiredFields = CONTAINER_ITEM_FIELDS.filter(field =>
        field.validator?.some(v => v.validator === CustomValidator.REQUIRED)
    );

    let optionalFields = CONTAINER_ITEM_FIELDS.filter(field =>
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

  }

  mapToDto(dialogResult: DynamicDialogResult): ContainerItemCreateInputDto {
    const main = dialogResult["main_section"]?.data || {};
    const second = dialogResult["second_section"]?.data || {};

    return {
      name: main.name,
      description: second.description ?? '',
      quantity: main.quantity,
      quantityType: main.quantityType,
      realPrice: main.realPrice,
      markup: second.markup ?? 0,
      markupRate: second.markupRate ?? 0,
      discount: second.discount ?? 0,
      discountRate: second.discountRate ?? 0,
      purchaseUrl: second.purchaseUrl ?? '',
      containerId: this.entityId!
    };
  }

  clickedElement(item: ContainerItemDto) {
    this.chosenElement = item;
  }

  async handleDoubleClick(item: ContainerItemDto) {
    this.chosenElement = item;
    await this.editItemContainer();
  }
}
