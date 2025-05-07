import {SubjektDetailDto} from '../../subjekty/data/subjekty.interfaces';
import {CustomValidator, FormField, FormFieldTypes, FormSelect} from '../../../shared/forms/form.interfaces';
import {InvoiceItemDto} from '../../polozky-faktury/data/polozky-faktury.interfaces';
import {ContainerDto} from '../../../sklady/sklady-polozky/data/sklady-polozky.interfaces';
import {ContainerItemDto} from '../../../sklady/polozka-kontejneru/data/polozka-kontejneru.interfaces';

export interface InvoiceCreateEditDto {
  invoiceNumber: string;
  supplierId: string;
  customerId: string;
  issueDate: string;
  taxDate?: string;
  deliveryDate?: string;
  totalNetAmount: number;
  totalVatAmount: number;
  totalGrossAmount: number;
  paymentTerms?: string;
  vatRate?: number;
  variableSymbol?: string;
  constantSymbol?: string;
  specificSymbol?: string;
}


export interface InvoiceDto {
  id?: string;
  invoiceNumber: string;
  supplierId: string;
  customerId: string;
  issueDate: string;
  taxDate?: string;
  deliveryDate?: string;
  totalNetAmount: number;
  totalVatAmount: number;
  totalGrossAmount: number;
  paymentTerms?: string;
  vatRate?: number;
  variableSymbol?: string;
  constantSymbol?: string;
  specificSymbol?: string;
}

export interface InvoiceDetailDto {
  id: string;
  invoiceNumber?: string;
  supplier: SubjektDetailDto;
  customer: SubjektDetailDto;
  issueDate: string;
  taxDate?: string;
  deliveryDate?: string;
  totalNetAmount: number;
  totalVatAmount: number;
  totalGrossAmount: number;
  paymentTerms?: string;
  vatRate?: number;
  variableSymbol?: string;
  constantSymbol?: string;
  specificSymbol?: string;
  items: InvoiceItemDto[];
}

export enum VatRate {
  None = 88,
  Standard21 = 83,
  Reduced15 = 82,
  Reduced12 = 76,
  Zero = 90,
  Exempt = 69,
  ReverseCharge = 67,
  NonVatPayer = 78
}

export const VatRateLabels: Record<VatRate, string> = {
  [VatRate.None]:           'Neuvedeno',
  [VatRate.Standard21]:     'Základní sazba 21 %',
  [VatRate.Reduced15]:      'Snížená sazba 15 %',
  [VatRate.Reduced12]:      'Snížená sazba 12 %',
  [VatRate.Zero]:           '0 % (bez DPH)',
  [VatRate.Exempt]:         'Osvobozeno od DPH',
  [VatRate.ReverseCharge]:  'Přenesená daňová povinnost',
  [VatRate.NonVatPayer]:    'Neplátce DPH'
};

export const VAT_RATE_OPTIONS: FormSelect[] = Object.values(VatRate)
  .filter(value => typeof value === 'number')
  .map((code) => ({
    value: code,
    displayValue: VatRateLabels[code as VatRate]
  }));

export const VAT_RATE_PERCENT: Record<VatRate, number> = {
  [VatRate.None]: 0,
  [VatRate.Standard21]: 21,
  [VatRate.Reduced15]: 15,
  [VatRate.Reduced12]: 12,
  [VatRate.Zero]: 0,
  [VatRate.Exempt]: 0,
  [VatRate.ReverseCharge]: 0,
  [VatRate.NonVatPayer]: 0,
};

export const CREATE_INVOICE_ITEM_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    formControlName: 'id',
    label: 'ID',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (i: InvoiceItemDto) => i.id,
    validator: [],
    visible: false
  },
  {
    formControlName: 'description',
    label: 'Popis položky',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (i: InvoiceItemDto) => i.description,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'quantity',
    label: 'Množství',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (i: InvoiceItemDto) => i.quantity,
    validator: [
      { validator: CustomValidator.REQUIRED },
      { validator: CustomValidator.MIN, params: 0 }
    ],
    visible: true
  },
  {
    formControlName: 'unitPrice',
    label: 'Cena za jednotku',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (i: InvoiceItemDto) => i.unitPrice,
    validator: [
      { validator: CustomValidator.REQUIRED },
      { validator: CustomValidator.MIN, params: 0 }
    ],
    visible: true
  },
  {
    formControlName: 'netAmount',
    label: 'Částka bez DPH',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (i: InvoiceItemDto) => i.netAmount,
    validator: [{ validator: CustomValidator.MIN, params: 0 }],
    visible: true
  },
  {
    formControlName: 'vatRate',
    label: 'Sazba DPH (%)',
    type: FormFieldTypes.LOOKUP,
    defaultValueGetter: (i: InvoiceItemDto) => i.vatRate,
    options: VAT_RATE_OPTIONS,
    validator: [
      { validator: CustomValidator.REQUIRED }
    ],
    visible: true
  },
  {
    formControlName: 'vatAmount',
    label: 'Částka DPH',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (i: InvoiceItemDto) => i.vatAmount,
    validator: [{ validator: CustomValidator.MIN, params: 0 }],
    visible: true
  },
  {
    formControlName: 'grossAmount',
    label: 'Celková částka',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (i: InvoiceItemDto) => i.grossAmount,
    validator: [{ validator: CustomValidator.MIN, params: 0 }],
    visible: true
  }
];

export const CREATE_EDIT_FAKTURA_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    formControlName: 'invoiceNumber',
    label: 'Číslo faktury',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.invoiceNumber,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'supplierId',
    label: 'Dodavatel',
    type: FormFieldTypes.AUTOCOMPLETE,
    defaultValueGetter: (s: InvoiceDto) => s.supplierId,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'customerId',
    label: 'Odběratel',
    type: FormFieldTypes.AUTOCOMPLETE,
    defaultValueGetter: (s: InvoiceDto) => s.customerId,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'issueDate',
    label: 'Datum vystavení',
    type: FormFieldTypes.DATE,
    defaultValueGetter: (s: InvoiceDto) => new Date(s.issueDate),
    validator: [
      { validator: CustomValidator.REQUIRED },
      { validator: CustomValidator.MIN_DATE, params: new Date('1000-05-01T15:40:54.115Z') },
    ],
    visible: true
  },
  {
    formControlName: 'taxDate',
    label: 'Datum zdanitelného plnění',
    type: FormFieldTypes.DATE,
    defaultValueGetter: (s: InvoiceDto) => s.taxDate ? new Date(s.taxDate) : null,
    validator: [
      { validator: CustomValidator.MIN_DATE, params: new Date('1000-05-01T15:40:54.115Z') },
    ],
    visible: true
  },
  {
    formControlName: 'deliveryDate',
    label: 'Datum dodání',
    type: FormFieldTypes.DATE,
    defaultValueGetter: (s: InvoiceDto) => s.deliveryDate ? new Date(s.deliveryDate) : null,
    validator: [
      { validator: CustomValidator.MIN_DATE, params: new Date('1000-05-01T15:40:54.115Z') },],
    visible: true
  },
  {
    formControlName: 'totalNetAmount',
    label: 'Částka bez DPH',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.totalNetAmount,
    validator: [{ validator: CustomValidator.REQUIRED }, { validator: CustomValidator.DECIMAL_PLACES, params: 2}],
    visible: true
  },
  {
    formControlName: 'totalVatAmount',
    label: 'DPH',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.totalVatAmount,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'totalGrossAmount',
    label: 'Částka s DPH',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.totalGrossAmount,
    validator: [{ validator: CustomValidator.REQUIRED },{validator: CustomValidator.DECIMAL_PLACES, params: 2}],
    visible: true
  },
  {
    formControlName: 'paymentTerms',
    label: 'Platební podmínky',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.paymentTerms,
    validator: [],
    visible: true
  },
  {
    formControlName: 'vatRate',
    label: 'Sazba DPH',
    type: FormFieldTypes.LOOKUP,
    defaultValueGetter: (s: InvoiceDto) => s.vatRate,
    options: VAT_RATE_OPTIONS,
    validator: [{ validator: CustomValidator.REQUIRED},{ validator: CustomValidator.MIN, params:0 }],
    visible: true
  },
  {
    formControlName: 'variableSymbol',
    label: 'Variabilní symbol',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.variableSymbol,
    validator: [],
    visible: true
  },
  {
    formControlName: 'constantSymbol',
    label: 'Konstantní symbol',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.constantSymbol,
    validator: [],
    visible: true
  },
  {
    formControlName: 'specificSymbol',
    label: 'Specifický symbol',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.specificSymbol,
    validator: [],
    visible: true
  }
];

export const ADD_POLOZKA_FROM_CONTAINER: Omit<FormField, 'defaultValue'>[] = [
  {
    visible: true,
    formControlName: 'containerId',
    label: 'Kontejner',
    type: FormFieldTypes.AUTOCOMPLETE,
    defaultValueGetter: (s: ContainerDto) => s.id,
    validator: [{ validator: CustomValidator.REQUIRED }],
  },
  {
    formControlName: 'containerItem',
    label: 'Položka z kontejneru',
    type: FormFieldTypes.AUTOCOMPLETE,
    defaultValueGetter: (s: ContainerItemDto) => s.id,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: false
  },
  {
    visible: false,
    formControlName: 'vatRate',
    label: 'Sazba dph',
    type: FormFieldTypes.LOOKUP,
    options: VAT_RATE_OPTIONS,
    defaultValueGetter: (s: InvoiceItemDto) => s.vatRate,
    validator: [{ validator: CustomValidator.REQUIRED }],
  },
  {
    visible: false,
    formControlName: 'quantity',
    label: 'Množství',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceItemDto) => s.quantity,
    validator: [
      { validator: CustomValidator.REQUIRED },
      { validator: CustomValidator.MIN, params: 1 },
    ],
  }
];


