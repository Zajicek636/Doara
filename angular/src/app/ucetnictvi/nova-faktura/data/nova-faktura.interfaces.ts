import {SubjektDetailDto} from '../../subjekty/data/subjekty.interfaces';
import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';
import {InvoiceItemDto} from '../../polozky-faktury/data/polozky-faktury.interfaces';

export interface InvoiceCreateEditDto {
  invoiceNumber: string;
  supplierId: string;
  customerId: string;
  issueDate: string; // ISO string
  taxDate: string; // ISO string
  deliveryDate: string; // ISO string
  totalNetAmount: number;
  totalVatAmount: number;
  totalGrossAmount: number;
  paymentTerms: string;
  vatRate: number;
  variableSymbol: string;
  constantSymbol: string;
  specificSymbol: string;
}


export interface InvoiceDto {
  id?: string;
  invoiceNumber: string;
  supplierId: string;
  customerId: string;
  issueDate: string;// ISO date string
  taxDate: string;
  deliveryDate: string;
  totalNetAmount: number;
  totalVatAmount: number;
  totalGrossAmount: number;
  paymentTerms: string;
  vatRate: number;
  variableSymbol: string;
  constantSymbol: string;
  specificSymbol: string;
}

export interface InvoiceDetailDto {
  id: string;
  invoiceNumber: string;
  supplier: SubjektDetailDto;
  customer: SubjektDetailDto;
  issueDate: string;
  taxDate: string;
  deliveryDate: string;
  totalNetAmount: number;
  totalVatAmount: number;
  totalGrossAmount: number;
  paymentTerms: string;
  vatRate: number;
  variableSymbol: string;
  constantSymbol: string;
  specificSymbol: string;
  items: InvoiceItemDto[];
}

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
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (i: InvoiceItemDto) => i.vatRate,
    validator: [
      { validator: CustomValidator.REQUIRED },
      { validator: CustomValidator.MIN, params: 0 }
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
      { validator: CustomValidator.MIN_DATE, params: new Date('2025-05-01T15:40:54.115Z') },
      { validator: CustomValidator.MAX_DATE, params: new Date('2025-06-02T15:40:54.115Z') }
    ],
    visible: true
  },
  {
    formControlName: 'taxDate',
    label: 'Datum zdanitelného plnění',
    type: FormFieldTypes.DATE,
    defaultValueGetter: (s: InvoiceDto) => new Date(s.taxDate),
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'deliveryDate',
    label: 'Datum dodání',
    type: FormFieldTypes.DATE,
    defaultValueGetter: (s: InvoiceDto) =>  new Date(s.deliveryDate),
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'totalNetAmount',
    label: 'totalNetAmount',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.totalNetAmount,
    validator: [],
    visible: true
  },
  {
    formControlName: 'totalVatAmount',
    label: 'totalvatamount',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.totalVatAmount,
    validator: [],
    visible: true
  },
  {
    formControlName: 'totalgrossamount',
    label: 'totalgrossamount',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.totalGrossAmount,
    validator: [],
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
    label: 'Sazba DPH (%)',
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (s: InvoiceDto) => s.vatRate,
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
