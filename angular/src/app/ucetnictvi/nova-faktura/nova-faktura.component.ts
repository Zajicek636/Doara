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
  CREATE_EDIT_FAKTURA_FIELDS,
  CREATE_INVOICE_ITEM_FIELDS,
  InvoiceCreateEditDto, InvoiceDetailDto, InvoiceDto,
} from './data/nova-faktura.interfaces';
import {SubjektDetailDto} from '../subjekty/data/subjekty.interfaces';
import {FormGroup} from '@angular/forms';
import {FormField} from '../../shared/forms/form.interfaces';
import {InvoiceItemDto} from '../polozky-faktury/data/polozky-faktury.interfaces';
import {PolozkyFakturyDataService} from '../polozky-faktury/data/polozky-faktury-data.service';
import {PolozkaKontejneruDataService} from '../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {DialogType, DynamicDialogResult} from '../../shared/dialog/dialog.interfaces';

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
  isBaseFormModified = false;

  invoiceItemForm: FormField[] = [];

  isInvoiceItemFormValid = false;
  invoiceItems: InvoiceItemDto[] = [];


  invoiceItemSectionToolbarButtons: ToolbarButton[] = []


  isNew = true;
  entity: InvoiceDto | null = null;
  baseForm!: FormGroup;
  subjektOptions:any[] = [];
  loaded: boolean = false;


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


  override async ngOnInit() {
    super.ngOnInit();
    this.route.paramMap.subscribe(pm => {
      this.entityId = pm.get('id');
      this.isNew = !this.entityId;
      this.refreshToolbarButtons();
    });

    await this.loadItemsForInits();
    await this.initSubjectsForm();
    await this.initInvocieItemsForm();

    await this.handleNewItem()


    this.loaded = true;
  }

  private async handleNewItem() {
    if (!this.isNew && this.entityId) {
      const faktura = await this.dataService.get(this.entityId)
      const supplierOption = this.subjektOptions.find(x => x.value === faktura.supplier.id);
      const customerOption = this.subjektOptions.find(x => x.value === faktura.customer.id);

      const mapped: InvoiceDto = {
        id: faktura.id,
        invoiceNumber: faktura.invoiceNumber,
        supplierId: supplierOption.value,
        customerId: customerOption.value,
        issueDate: faktura.issueDate,
        taxDate: faktura.taxDate,
        deliveryDate: faktura.deliveryDate,
        totalNetAmount: faktura.totalNetAmount,
        totalVatAmount: faktura.totalVatAmount,
        totalGrossAmount: faktura.totalGrossAmount,
        paymentTerms: faktura.paymentTerms,
        vatRate: faktura.vatRate,
        variableSymbol: faktura.variableSymbol,
        constantSymbol: faktura.constantSymbol,
        specificSymbol: faktura.specificSymbol,
      }
      this.baseFormFields = populateDefaults(this.baseFormFields, mapped);
      this.invoiceItems.push(...faktura.items)
    }
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
    this.baseFormFields = CREATE_EDIT_FAKTURA_FIELDS.map(f => {
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
    this.baseForm = form;
    form.get('supplierId')?.valueChanges.subscribe(val => {
      this.syncFields('supplierId', 'customerId', val);
    });

    form.get('customerId')?.valueChanges.subscribe(val => {
      this.syncFields('customerId', 'supplierId', val);
    });
  }
  async addNewPolozkaFaktury() {
    const newItem = populateDefaults(CREATE_INVOICE_ITEM_FIELDS, {})
    const response = await this.dialogService.form({
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

    if(!response) return;

    const newPolozka: InvoiceItemDto = this.mapToInvoiceCreateDto(response)
    newPolozka.id = newPolozka.id == '' ? `newPolozka${this.invoiceItems.length}` : newPolozka.id
    this.invoiceItems.push(newPolozka as InvoiceItemDto)
  }

  async editPolozka(item: InvoiceItemDto, index: number) {
    const newItem = populateDefaults(CREATE_INVOICE_ITEM_FIELDS, item)
    const response = await this.dialogService.form({
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

    if(!response) return;

    const updatedItem = this.mapToInvoiceCreateDto(response)
    this.invoiceItems[index] = updatedItem as InvoiceItemDto;
  }

  private mapToInvoiceCreateDto(response: DynamicDialogResult): InvoiceItemDto {
    const main =  response["main_section"]?.data || {}
    return {
      id: main.id,
      description: main.description,
      quantity: main.quantity,
      unitPrice: main.unitPrice,
      vatRate: main.vatRate,
      netAmount: main.netAmount,
      vatAmount: main.vatAmount,
      grossAmount: main.grossAmount,
    }
  }

  private syncFields(changedKey: 'supplierId' | 'customerId', affectedKey: 'supplierId' | 'customerId', changedValue: any) {
    const affectedValue = this.baseForm.get(affectedKey)?.value;
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
      this.baseForm.get(affectedKey)?.setValue(null);
    }

    this.baseFormFields = populateDefaults(this.baseFormFields, this.baseForm.value);
  }

  onBaseFormChanged(result: FormComponentResult) {
    this.isBaseFormValid = result.valid;
    this.isBaseFormModified = result.modified;
    this.baseForm = result.form;
    const data = result.data as InvoiceCreateEditDto;
  }

  async saveFaktura() {
    await this.router.navigate(['ucetnictvi', 'faktura', "10"]);
  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
}
