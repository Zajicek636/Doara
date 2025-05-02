import {Component, OnInit, ViewChild} from '@angular/core';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {
  CountryDto,
  SUBJEKT_ADDRESS_FIELDS,
  SUBJEKT_BASE_FIELDS, SubjektCreateEditDto,
  SubjektDetailDto,
  SubjektyDialogResult
} from './data/subjekty.interfaces';
import {SubjektyDataService} from './data/subjekty-data.service';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {SharedModule} from '../../shared/shared.module';
import {FormGroup} from '@angular/forms';
import {FormField} from '../../shared/forms/form.interfaces';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {populateDefaults} from '../../shared/forms/form-field.utils';
import {SubjektyModalComponent} from './subjekty-modal.component';
import {ZemeDataService} from "../zeme/data/zeme-data.service";
import {PagedList} from "../../shared/crud/base-crud-service";

@Component({
  selector: 'app-subjekty',
  imports: [
    SharedModule
  ],
  templateUrl: './subjekty.component.html',
  styleUrl: './subjekty.component.scss'
})
export class SubjektyComponent  extends BaseContentComponent<SubjektDetailDto, SubjektyDataService> implements OnInit {
  @ViewChild(DynamicTableComponent) tableComponent!: DynamicTableComponent<SubjektDetailDto>;

  constructor(
    protected override dataService: SubjektyDataService,
    protected countryDataService: ZemeDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute
  ) {
    super(route, router,breadcrumbService, dialogService, dataService);

  }

  form!: FormGroup;
  subjektInfoFields!: FormField[];
  subjektAddressFields!: FormField[];
  countries!: PagedList<CountryDto>;


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
    this.loadData()
  }

  async loadData() {
    this.countries = await this.countryDataService.getAll()
  }

  protected buildToolbarButtons(): ToolbarButton[] {
    return [
      {
        id: 'add',
        text: 'Přidat',
        icon: BaseMaterialIcons.ADD_PERSON,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => this.addSubjekt()
      },

      {
        id: 'edit',
        text: 'Upravit',
        icon: BaseMaterialIcons.EDIT_PERSON,
        class: 'btn-secondary',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.editSubjekt()
      },
      {
        id: 'delete',
        text: 'Smazat',
        icon: BaseMaterialIcons.REMOVE_PERSON,
        class: 'btn-danger',
        disabled: !this.chosenElement,
        visible: true,
        action: () => this.deleteSubjekt()
      }
    ];
  }

  async deleteSubjekt() {
    if(!this.chosenElement) return;
    const res = await this.dialogService.confirmAsync({
      title: "Potvrzení smazání",
      icon: BaseMaterialIcons.REMOVE_PERSON,
      message: `Opravdu chcete odebrat subjekt: <strong>${this.chosenElement.name} - ${this.chosenElement.ic}</strong> ?`,
      dialogType: DialogType.ALERT,
      cancelButton: "Ne",
      confirmButton: "Ano"
    })
    if(!res) return;

    try {
      await this.dataService.delete(this.chosenElement.id)
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

  async editSubjekt() {
    if(!this.chosenElement) return;

    const addressFieldsWithOpts = SUBJEKT_ADDRESS_FIELDS.map(f => {
      if (f.formControlName === 'SubjektCountryCode') {
        return {
          ...f,
          options: this.countries.items.map(c => ({
            value: c.id,
            displayValue: c.name
          }))
        };
      }
      return f;
    });

    this.subjektInfoFields = populateDefaults(SUBJEKT_BASE_FIELDS,this.chosenElement)
    this.subjektAddressFields = populateDefaults(addressFieldsWithOpts,this.chosenElement)

    const dialogResult: SubjektyDialogResult = await this.dialogService.open(
      SubjektyModalComponent<SubjektyDialogResult>,
      {
        icon: BaseMaterialIcons.EDIT_PERSON,
        title: "Editace subjektu",
        subjektBaseFields: this.subjektInfoFields,
        additionalSubjektFields: this.subjektAddressFields,
        type: DialogType.SUCCESS
      }
    )

    if(!dialogResult) return;
    try {
      const result = this.mapToDto(dialogResult);
      const res = await this.dataService.put(result.id!,result, {useSuffix: true});
     /* this.tableComponent.dataSource.data = this.tableComponent.dataSource.data.map(x =>
        x.id === result.id ? result : x
      );*/
    } catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e,
        dialogType: DialogType.ERROR
      })
    }
  }

  async addSubjekt() {
    this.subjektInfoFields = SUBJEKT_BASE_FIELDS
    this.subjektAddressFields = SUBJEKT_ADDRESS_FIELDS
    const codeField = this.subjektAddressFields
        .find(f => f.formControlName === 'SubjektCountryCode');
    if (codeField) {
      codeField.options = this.countries.items.map(c => ({
        value: c.code,
        displayValue: c.name
      }));
    }

    const dialogResult: SubjektyDialogResult = await this.dialogService.open(
      SubjektyModalComponent<SubjektyDialogResult>,
      {
        icon: BaseMaterialIcons.ADD_PERSON,
        title: "Nový subjekt",
        subjektBaseFields: this.subjektInfoFields,
        additionalSubjektFields: this.subjektAddressFields,
        type: DialogType.SUCCESS
      }
    )
    if(!dialogResult) return;

    const newSubjekt = this.mapToDto(dialogResult);
    const res = await this.dataService.post('', newSubjekt, {useSuffix: true});
    const data = this.tableComponent.dataSource.data;
    this.tableComponent.dataSource.data = [...data, res];

  }

  public async handleDoubleClick(row: SubjektDetailDto) {
    this.chosenElement = row
    await this.editSubjekt()
  }

  public clickedElement(row: SubjektDetailDto) {
    console.log('clickedElement', row);
    this.chosenElement = row;
  }

  private mapToDto(dialogResult: SubjektyDialogResult): SubjektCreateEditDto {
    const base = dialogResult.subjektBaseResult.data;
    const addr = dialogResult.subjektAddressResult.data;
    const selectedCountry = this.countries.items.find(c => c.id === addr.SubjektCountryCode.value);
    return {
      id: base.SubjektId,
      name: base.SubjektName,
      ic:   base.SubjektIc,
      dic:  base.SubjektDic,
      isVatPayer: base.SubjektPayer,
      address: {
        id: addr.AddressId ?? '',
        street: addr.SubjektStreet,
        city:   addr.SubjektCity,
        postalCode: addr.SubjektPSC,
        countryId: selectedCountry?.id!
      }
    };
  }
}
