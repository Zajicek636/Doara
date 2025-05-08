import {Component, OnDestroy, OnInit, TemplateRef, ViewChild} from '@angular/core';
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
import {populateDefaults, round} from '../../shared/forms/form-field.utils';
import {
  CREATE_EDIT_FAKTURA_FIELDS,
  CREATE_INVOICE_ITEM_FIELDS,
  InvoiceCreateEditDto,
  InvoiceDto,
  VAT_RATE_PERCENT,
  VatRate, VatRateLabels,
} from './data/nova-faktura.interfaces';
import {SubjektDetailDto} from '../subjekty/data/subjekty.interfaces';
import {FormGroup} from '@angular/forms';
import {FormField} from '../../shared/forms/form.interfaces';
import {InvoiceItemCreateManyDto, InvoiceItemDto} from '../polozky-faktury/data/polozky-faktury.interfaces';
import {PolozkaKontejneruDataService} from '../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {DialogType, DynamicDialogResult} from '../../shared/dialog/dialog.interfaces';
import {NovaPolozkaKontejnerModal} from './nova-polozka-kontejner-modal';
import {SkladyPolozkyDataService} from '../../sklady/sklady-polozky/data/sklady-polozky-data.service';
import {DrawerService} from '../../shared/layout/drawer.service';
import {DecimalPipe} from '@angular/common';
import {PolozkyFakturyDataService} from '../polozky-faktury/data/polozky-faktury-data.service';

@Component({
  selector: 'app-nova-faktura',
  imports: [SharedModule, DecimalPipe],
  templateUrl: './nova-faktura.component.html',
  styleUrl: './nova-faktura.component.scss'
})
export class NovaFakturaComponent extends BaseContentComponent<any,any> implements OnInit, OnDestroy {
  @ViewChild('drawerContent') drawerTemplate!: TemplateRef<any>;


  baseFormFields: FormField[] = [];
  isBaseFormValid = false;
  isBaseFormModified = false;

  invoiceItemForm: FormField[] = [];

  isInvoiceItemFormValid = false;
  invoiceItems: InvoiceItemDto[] = [];
  invoiceItemsForDelete: string[] = []


  invoiceItemSectionToolbarButtons: ToolbarButton[] = []


  isNew = true;
  entity: InvoiceDto | null = null;
  baseForm!: FormGroup;
  subjektOptions:any[] = [];
  loaded: boolean = false;
  drawerOpen = false;


  constructor(
    private subjektyDataService: SubjektyDataService,
    protected override dataService: NovaFakturaDataService,
    protected override breadcrumbService: BreadcrumbService,
    protected override router: Router,
    protected override dialogService: DialogService,
    protected override route: ActivatedRoute,
    private containerItemsDataService: PolozkaKontejneruDataService,
    private skladyPolozkyDataService: SkladyPolozkyDataService,
    private polozkyFakturyDataService: PolozkyFakturyDataService,
    private drawerService: DrawerService
  ) {
    super(route, router, breadcrumbService, dialogService, dataService);
  }

  override set chosenElement(value: any) {
    super.chosenElement = value;
  }


  override async ngOnInit() {
    super.ngOnInit();
    this.isNew = !this.entityId;

    await this.loadItemsForInits();
    await this.initSubjectsForm();
    await this.initInvocieItemsForm();

    await this.handleNewItem()
    this.loaded = true;
  }

  ngOnDestroy() {
    this.drawerService.close()
  }

  private async handleNewItem() {
    if (!this.isNew && this.entityId) {
      const faktura = await this.dataService.get(this.entityId)
      const supplierOption = this.subjektOptions.find(x => x.value === faktura.supplier.id);
      const customerOption = this.subjektOptions.find(x => x.value === faktura.customer.id);

      const mapped: InvoiceDto = {
        id: faktura.id,
        invoiceNumber: faktura.invoiceNumber!,
        supplierId: supplierOption.value,
        customerId: customerOption.value,
        issueDate: faktura.issueDate,
        taxDate: faktura.taxDate!,
        deliveryDate: faktura.deliveryDate!,
        totalNetAmount: faktura.totalNetAmount,
        totalVatAmount: faktura.totalVatAmount,
        totalGrossAmount: faktura.totalGrossAmount,
        paymentTerms: faktura.paymentTerms!,
        vatRate: faktura.vatRate!,
        variableSymbol: faktura.variableSymbol!,
        constantSymbol: faktura.constantSymbol!,
        specificSymbol: faktura.specificSymbol!,
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
        tooltip: "Vytvořit položku",
        disabled: false,
        action: () => this.addNewPolozkaFaktury()
      },
      {
        id: 'newPolozkaSkladu',
        text: 'Přidat položku ze skladu',
        icon: BaseMaterialIcons.ADD_CONTAINER,
        class: 'btn-primary',
        tooltip: "Přidat položku ze skladu",
        visible: !this.isNew,
        disabled: false,
        action: () => this.addNewPolozkaFromContainer()
      }
    ];

    return [
      {
        id: 'saveFaktura',
        text: 'Uložit',
        icon: BaseMaterialIcons.SAVE,
        class: 'btn-primary',
        visible: true,
        disabled: this.baseForm?.invalid ?? true,
        action: () => this.saveFaktura()
      },
      {
        id: 'polozkyFaktura',
        text: 'Položky faktury',
        icon: BaseMaterialIcons.RECEIPT,
        class: 'btn-secondary',
        visible: true,
        disabled: this.isNew,
        action: () => {this.toggleDrawerWithContent()}
      },
      {
        id: 'cancelFaktura',
        text: 'Zrušit',
        icon: BaseMaterialIcons.CANCEL,
        class: 'btn-secondary',
        visible: true,
        disabled: this.baseForm?.dirty ?? true,
        action: () => {this.baseForm.reset()}
      }
    ];
  }

  toggleDrawerWithContent() {
    if (this.drawerOpen) {
      this.drawerService.close();
      this.drawerOpen = false;
    } else {
      this.drawerService.openWithTemplate(this.drawerTemplate);
      this.drawerOpen = true;
    }
  }

  async addNewPolozkaFromContainer() {
    const items = await this.skladyPolozkyDataService.getAll(undefined, {useSuffix: true});
    const res: InvoiceItemDto = await this.dialogService.open(NovaPolozkaKontejnerModal<FormComponentResult>,
      {
        icon: BaseMaterialIcons.ADD_CONTAINER,
        title: "Nová položka z kontejneru",
        type: DialogType.SUCCESS,
        containers: items,
    })
    if(!res) return
    this.invoiceItems.push(res)
  }

  onBaseFormReady(form: FormGroup) {
    this.baseForm = form;
    this.bindSubjectChange()
    this.bindVatCalculation();
  }

  bindSubjectChange() {
    this.baseForm.get('supplierId')?.valueChanges.subscribe(val => {
      this.syncFields('supplierId', 'customerId', val);
    });

    this.baseForm.get('customerId')?.valueChanges.subscribe(val => {
      this.syncFields('customerId', 'supplierId', val);
    });
  }

  bindVatCalculation() {
    const netCtrl = this.baseForm.get('totalNetAmount')!;
    const vatCtrl = this.baseForm.get('totalVatAmount')!;
    const grossCtrl = this.baseForm.get('totalGrossAmount')!;
    const rateCtrl = this.baseForm.get('vatRate')!;

    function getRate(): number {
      const rateKey = rateCtrl.value.value as VatRate;
      return VAT_RATE_PERCENT[rateKey] ?? 0;
    }

    netCtrl.valueChanges.subscribe(netRaw => {
      const rate = getRate();
      if (rate == null) return;

      const net = parseFloat(netRaw) || 0;
      const vat = net * rate / 100;
      const gross = net + vat;

      vatCtrl.setValue(round(vat, 2), { emitEvent: false });
      grossCtrl.setValue(round(gross, 2), { emitEvent: false });
    });

    grossCtrl.valueChanges.subscribe(grossRaw => {
      const rate = getRate();
      if (rate == null) return;

      const gross = parseFloat(grossRaw) || 0;
      const net = gross / (1 + rate / 100);
      const vat = gross - net;

      vatCtrl.setValue(round(vat, 2), { emitEvent: false });
      netCtrl.setValue(round(net, 2), { emitEvent: false });
    });

    rateCtrl.valueChanges.subscribe(() => {
      const rate = getRate();
      if (rate == null) return;

      const net = parseFloat(netCtrl.value) || 0;
      const gross = parseFloat(grossCtrl.value) || 0;

      if (net) {
        const vat = net * rate / 100;
        vatCtrl.setValue(round(vat, 2), { emitEvent: false });
        grossCtrl.setValue(round(net + vat, 2), { emitEvent: false });
      } else if (gross) {
        const netCalc = gross / (1 + rate / 100);
        const vat = gross - netCalc;
        vatCtrl.setValue(round(vat, 2), { emitEvent: false });
        netCtrl.setValue(round(netCalc, 2), { emitEvent: false });
      }
    });
  }

  getVatRateLabel(rate: number) {
    return VatRateLabels[rate as VatRate];
  }

  async addNewPolozkaFaktury() {
    const newItem = populateDefaults(CREATE_INVOICE_ITEM_FIELDS, {});

    const responsePromise = this.dialogService.form({
      headerIcon: BaseMaterialIcons.PLUS,
      title: `Nová položka faktury`,
      sections: [{
        sectionId: "main_section",
        headerIcon: BaseMaterialIcons.ASSIGNMENT,
        fields: newItem,
        sectionTitle: "Informace o položce"
      }],
      type: DialogType.SUCCESS
    }, (sectionId, form) => {
      if (sectionId !== 'main_section') return;

      const unitCtrl = form.get('unitPrice');
      const qtyCtrl = form.get('quantity');
      const vatRateCtrl = form.get('vatRate');
      const netCtrl = form.get('netAmount');
      const vatCtrl = form.get('vatAmount');
      const grossCtrl = form.get('grossAmount');

      const recalculate = () => {
        const unitPrice = parseFloat(unitCtrl?.value) || 0;
        const quantity = parseFloat(qtyCtrl?.value) || 0;
        const vatRate = vatRateCtrl?.value?.value as VatRate;
        const vatPercent = VAT_RATE_PERCENT[vatRate] ?? 0;

        const net = unitPrice * quantity;
        const vat = net * vatPercent / 100;
        const gross = net + vat;

        netCtrl?.setValue(round(net, 2), { emitEvent: false });
        vatCtrl?.setValue(round(vat, 2), { emitEvent: false });
        grossCtrl?.setValue(round(gross, 2), { emitEvent: false });
      };

      unitCtrl?.valueChanges.subscribe(recalculate);
      qtyCtrl?.valueChanges.subscribe(recalculate);
      vatRateCtrl?.valueChanges.subscribe(recalculate);
    });

    const response = await responsePromise;
    if (!response) return;

    const newPolozka: InvoiceItemDto = this.mapToInvoiceItemCreateDto(response);
    newPolozka.id = newPolozka.id == '' ? undefined : newPolozka.id;
    this.invoiceItems.push(newPolozka as InvoiceItemDto);
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

    const updatedItem = this.mapToInvoiceItemCreateDto(response)
    this.invoiceItems[index] = updatedItem as InvoiceItemDto;
  }

  countSum() {
    return this.invoiceItems.reduce((sum, item) => sum + (item.grossAmount || 0), 0);
  }

  async removePolozka(item: InvoiceItemDto, index: number) {
    console.log(item)
    const res = await this.dialogService.confirmAsync({
      dialogType: DialogType.ALERT,
      title: "Potvrdit operaci",
      message: "Chcete odebrat položku z faktury?",
      icon: BaseMaterialIcons.DELETE
    })

    if(!res) return;
    const existringItem = this.invoiceItems.find(x=>x?.id === item.id);
    if(existringItem?.id) {
      this.invoiceItemsForDelete.push(existringItem.id!);
    }
    this.invoiceItems.splice(index, 1);
  }

  private mapToInvoiceItemCreateDto(response: DynamicDialogResult): InvoiceItemDto {
    const main =  response["main_section"]?.data || {}
    return {
      id: main.id,
      description: main.description,
      quantity: main.quantity,
      unitPrice: main.unitPrice,
      vatRate: main.vatRate.value,
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

    //this.baseFormFields = populateDefaults(this.baseFormFields, this.baseForm.value);
  }

  onBaseFormChanged(result: FormComponentResult) {
    this.isBaseFormValid = result.valid;
    this.isBaseFormModified = result.modified;
    this.baseForm = result.form;

    this.refreshToolbarButtons();
  }

  async saveFaktura() {
    if(this.entityId == '' && this.isNew) {
      await this.postNewFaktura()
    } else {
      await this.postEditFaktura()
    }
  }

  async postEditFaktura() {
    const faktura = this.mapToInvoiceDtoFromForm();
    try {
      await this.dataService.put(this.entityId!, faktura);
      const invoiceItems: InvoiceItemCreateManyDto = {
        invoiceId: this.entityId!,
        items: this.invoiceItems,
        itemsForDelete: this.invoiceItemsForDelete
      }
      const items = await this.polozkyFakturyDataService.post('', invoiceItems, {useSuffix: true})
    }
    catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e.error.error.message,
        dialogType: DialogType.ERROR,
      });
    }
  }

  async postNewFaktura() {
    const newFaktura = this.mapToInvoiceDtoFromForm()
    const res = await this.dataService.post('',newFaktura)
    await this.router.navigate(['ucetnictvi', 'faktura', res.id]);
  }

  private mapToInvoiceDtoFromForm(): InvoiceCreateEditDto {
    return {
      invoiceNumber: this.baseForm.value.invoiceNumber,
      supplierId: this.baseForm.value.supplierId.value,
      customerId: this.baseForm.value.customerId.value,
      issueDate: this.baseForm.value.issueDate,
      taxDate: this.baseForm.value.taxDate ?? null,
      deliveryDate: this.baseForm.value.deliveryDate ?? null,
      totalNetAmount: this.baseForm.value.totalNetAmount,
      totalVatAmount: this.baseForm.value.totalVatAmount,
      totalGrossAmount: this.baseForm.value.totalGrossAmount,
      paymentTerms: this.baseForm.value.paymentTerms ?? null,
      vatRate: this.baseForm.value.vatRate?.value ?? null,
      variableSymbol: this.baseForm.value.variableSymbol ?? null,
      constantSymbol: this.baseForm.value.constantSymbol ?? null,
      specificSymbol: this.baseForm.value.specificSymbol ?? null
    };

  }
  protected readonly BaseMaterialIcons = BaseMaterialIcons;
  protected readonly VatRateLabels = VatRateLabels;
  protected readonly VatRate = VatRate;
}
