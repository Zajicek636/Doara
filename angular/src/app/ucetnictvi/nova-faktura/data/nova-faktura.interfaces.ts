import {SubjektDetailDto} from '../../subjekty/data/subjekty.interfaces';
import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';
import {InvoiceDto} from '../../seznam-faktur/data/seznam-faktur.interfaces';

export interface InvoiceCreateDto {
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


export const CREATE_FAKTURA_FIELDS: Omit<FormField, 'defaultValue'>[] = [
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
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.issueDate,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'taxDate',
    label: 'Datum zdanitelného plnění',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.taxDate,
    validator: [{ validator: CustomValidator.REQUIRED }],
    visible: true
  },
  {
    formControlName: 'deliveryDate',
    label: 'Datum dodání',
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: InvoiceDto) => s.deliveryDate,
    validator: [{ validator: CustomValidator.REQUIRED }],
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
    validator: [{ validator: CustomValidator.MIN, params: 0 }],
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
