import {Component, OnInit, ViewChild} from '@angular/core';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {
  SUBJEKT_ADDRESS_FIELDS,
  SUBJEKT_BASE_FIELDS,
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
    await this.dialogService.confirmAsync({
      title: "Potvrzení smazání",
      icon: BaseMaterialIcons.REMOVE_PERSON,
      message: `Opravdu chcete odebrat subjekt: <strong>${this.chosenElement.Name} - ${this.chosenElement.Ic}</strong> ?`,
      dialogType: DialogType.ALERT,
      cancelButton: "Ne",
      confirmButton: "Ano"
    })
  }

  async editSubjekt() {
    if(!this.chosenElement) return;

    this.subjektInfoFields = populateDefaults(SUBJEKT_BASE_FIELDS,this.chosenElement)
    this.subjektAddressFields = populateDefaults(SUBJEKT_ADDRESS_FIELDS,this.chosenElement)

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
      //this.dialogService.put(this.chosenElement.id,)
      this.tableComponent.dataSource.data = this.tableComponent.dataSource.data.map(x =>
        x.id === result.id ? result : x
      );
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

    console.log(dialogResult)
    const newSubjekt = this.mapToDto(dialogResult);
    const data = this.tableComponent.dataSource.data;
    this.tableComponent.dataSource.data = [...data, newSubjekt];
    //await this.dataService.post("123",a)

  }

  public handleDoubleClick(row: SubjektDetailDto) {
  }

  public clickedElement(row: SubjektDetailDto) {
    console.log('clickedElement', row);
    this.chosenElement = row;
  }

  private mapToDto(dialogResult: SubjektyDialogResult) {
    const { subjektBaseResult, subjektAddressResult } = dialogResult;
    const base = subjektBaseResult.data;
    const addr = subjektAddressResult.data;

    const newSubjekt: SubjektDetailDto = {
      id: base.SubjektId,
      Name: base.SubjektName,
      Ic:   base.SubjektIc,
      Dic:  base.SubjektDic,
      IsVatPayer: base.SubjektPayer,
      AddressDetailDto: {
        id: addr.AddressId,
        Street: addr.SubjektStreet,
        City:   addr.SubjektCity,
        PostalCode: addr.SubjektPSC,
        CountryDto: {
          id: addr.CountryId,
          Name: addr.SubjektCountryName,
          Code: addr.SubjektCountryCode
        }
      }
    };
    return newSubjekt;
  }
}
