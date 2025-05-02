import {Component, OnInit} from '@angular/core';
import {BaseContentComponent} from '../../shared/layout/base-component';
import {SubjektyDataService} from '../subjekty/data/subjekty-data.service';
import {BreadcrumbService} from '../../shared/breadcrumb/breadcrumb.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DialogService} from '../../shared/dialog/dialog.service';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {SharedModule} from '../../shared/shared.module';
import {NovaFakturaDataService} from './data/nova-faktura-data.service';
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {populateDefaults} from '../../shared/forms/form-field.utils';
import {
  CREATE_FAKTURA_FIELDS,
  CREATE_INVOICE_ITEM_FIELDS,
  InvoiceCreateEditDto, InvoiceDto,
} from './data/nova-faktura.interfaces';
import {SubjektDetailDto} from '../subjekty/data/subjekty.interfaces';
import {FormGroup} from '@angular/forms';
import {FormField} from '../../shared/forms/form.interfaces';
import {InvoiceItemDto} from '../polozky-faktury/data/polozky-faktury.interfaces';
import {PolozkyFakturyDataService} from '../polozky-faktury/data/polozky-faktury-data.service';
import {PolozkaKontejneruDataService} from '../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {ContainerDto, CREATE_CONTAINER_FIELDS} from '../../sklady/sklady-polozky/data/sklady-polozky.interfaces';
import {DialogType} from '../../shared/dialog/dialog.interfaces';

@Component({
  selector: 'app-nova-faktura',
  imports: [SharedModule],
  templateUrl: './nova-faktura.component.html',
  styleUrl: './nova-faktura.component.scss'
})
export class NovaFakturaComponent extends BaseContentComponent<any,any> implements OnInit {
  entityId: string | null = null;

  baseFormFields: FormField[] = [];
  isBaseFormValid = false;

  invoiceItemForm: FormField[] = [];
  isInvoiceItemFormValid = false;
  availableInvoiceItems: InvoiceItemDto[] = []

  invoiceItemSectionToolbarButtons: ToolbarButton[] = []


  constructor(
    private subjektyDataService: SubjektyDataService,
    protected override dataService: NovaFakturaDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute,
    private polozkyFakturyDataService: PolozkyFakturyDataService,
    private containerItemsDataService: PolozkaKontejneruDataService,
  ) {
    super(route, router, breadcrumbService, dialogService, dataService);
  }

  override set chosenElement(value: any) {
    super.chosenElement = value;
  }

  defaultValues: any = {};
  isNew = true;
  entity: InvoiceDto | null = null;
  formGroup!: FormGroup;
  subjektOptions:any[] = [];
  loaded: boolean = false;

  override async ngOnInit() {
    super.ngOnInit();
    this.route.paramMap.subscribe(pm => {
      this.entityId = pm.get('id');
      this.isNew    = !this.entityId;
      this.refreshToolbarButtons();
    });

    await this.loadItemsForInits();
    await this.initSubjectsForm();
    await this.initInvocieItemsForm();

    if (!this.isNew && this.entityId) {
      // Mock data pro editaci stávající faktury
      const mock: InvoiceDto = {
        id: this.entityId,
        invoiceNumber: 'F2025EDIT',
        supplierId: this.subjektOptions[0]?.value!,
        customerId: this.subjektOptions[1]?.value!,
        issueDate:    '2025-05-01T00:00:00.000Z',
        taxDate:      '2025-05-02T00:00:00.000Z',
        deliveryDate: '2025-05-03T00:00:00.000Z',
        totalNetAmount:   1234,
        totalVatAmount:    234,
        totalGrossAmount:  1468,
        paymentTerms:   '30 dní',
        vatRate:        21,
        variableSymbol: '2025001',
        constantSymbol: '0308',
        specificSymbol: '001'
      };

      // Naplň defaultValues a přegeneruj baseFormFields
      this.defaultValues    = mock;
      this.baseFormFields   = populateDefaults(this.baseFormFields, mock);
    }
    this.loaded = true;
  }

  private async loadItemsForInits() {
    // subjects
    const page = await this.subjektyDataService.getAll();
    const subjects = page.items;
    this.subjektOptions = subjects.map((s: SubjektDetailDto) => ({
      value: s.id,
      displayValue: `${s.name} – ${s.ic} – ${s.dic}`
    }));
  }

  private async initInvocieItemsForm() {
    this.invoiceItemForm = CREATE_INVOICE_ITEM_FIELDS
  }

  private async initSubjectsForm() {

    //options pro customerField a supplierField
    this.baseFormFields = CREATE_FAKTURA_FIELDS.map(f => {
      if (f.formControlName === 'supplierId' || f.formControlName === 'customerId') {
        return { ...f, options: this.subjektOptions };
      }
      return f;
    });
  }

  override buildToolbarButtons(): ToolbarButton[] {
    this.invoiceItemSectionToolbarButtons = [
      {
        id: 'newPolozka',
        text: 'Vytvořit položku',
        icon: BaseMaterialIcons.PLUS,
        class: 'btn-primary',
        visible: !this.isNew,
        disabled: false,
        action: () => this.addNewPolozkaFaktury()
      },
      {
        id: 'newPolozkaSkladu',
        text: 'Přidat položku ze skladu',
        icon: BaseMaterialIcons.ADD_CONTAINER,
        class: 'btn-primary',
        visible: !this.isNew,
        disabled: false,
        action: () => {  }
      }
    ];

    return [
      {
        id: 'saveFaktura',
        text: 'Uložit',
        icon: BaseMaterialIcons.SAVE,
        class: 'btn-primary',
        visible: true,
        disabled: false,
        action: () => this.saveFaktura()
      },
      {
        id: 'cancelFaktura',
        text: 'Zrušit',
        icon: BaseMaterialIcons.CANCEL,
        class: 'btn-secondary',
        visible: true,
        disabled: false,
        action: () => this.router.navigate(['/seznam-faktur'])
      }
    ];
  }

  onBaseFormReady(form: FormGroup) {
    this.formGroup = form;
    form.get('supplierId')?.valueChanges.subscribe(val => {
      this.syncFields('supplierId', 'customerId', val);
    });

    form.get('customerId')?.valueChanges.subscribe(val => {
      this.syncFields('customerId', 'supplierId', val);
    });
  }
  async addNewPolozkaFaktury() {
    const newItem = populateDefaults(CREATE_INVOICE_ITEM_FIELDS, {})

    const a = await this.dialogService.form({
      headerIcon: BaseMaterialIcons.PLUS,
      title: `Nová položka faktury`,
      sections: [
        {
          sectionId: "main_section",
          headerIcon: BaseMaterialIcons.ASSIGNMENT,
          fields: newItem,
          sectionTitle: "Informace o položce"
        }],
      type: DialogType.SUCCESS
    })

  }
  private syncFields(changedKey: 'supplierId' | 'customerId', affectedKey: 'supplierId' | 'customerId', changedValue: any) {
    const affectedValue = this.formGroup.get(affectedKey)?.value;
    const updatedOptions = this.subjektOptions.filter(
      s => s.value !== changedValue?.value
    );

    this.baseFormFields = this.baseFormFields.map(f => {
      if (f.formControlName === affectedKey) {
        return {
          ...f,
          options: updatedOptions
        };
      }
      return f;
    });
    if (changedValue?.value && affectedValue === changedValue.value) {
      this.formGroup.get(affectedKey)?.setValue(null);
    }

    this.baseFormFields = populateDefaults(this.baseFormFields, this.formGroup.value);
  }

  onBaseFormChanged(result: FormComponentResult) {
    this.isBaseFormValid = result.valid;
    this.isBaseFormValid = result.modified;
    this.formGroup = result.form;
    const data = result.data as InvoiceCreateEditDto;
  }

  async saveFaktura() {
    await this.router.navigate(['ucetnictvi', 'faktura', "10"]);
  }

}
