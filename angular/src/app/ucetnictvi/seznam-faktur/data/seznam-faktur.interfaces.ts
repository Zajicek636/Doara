
import {ColumnSetting} from '../../../shared/table/table/table.settings';
import {InvoiceDto, VatRate, VatRateLabels} from '../../nova-faktura/data/nova-faktura.interfaces';
import {BaseMaterialIcons} from '../../../../styles/material.icons';

export const INVOICE_COLUMNS: ColumnSetting<InvoiceDto>[] = [
  { key: 'invoiceNumber', header: 'Doklad', isReference: true, referenceAsIcon: true, referenceIcon: BaseMaterialIcons.NEW_QUOTE, valueGetter: r => r.invoiceNumber },
  { key: 'issueDate', header: 'Datum vystavení', valueGetter: r => r.issueDate ? new Date(r.issueDate).toLocaleDateString() : '' },
  { key: 'taxDate', header: 'Datum DPH', valueGetter: r => r.taxDate ? new Date(r.taxDate).toLocaleDateString() : '' },
  { key: 'deliveryDate', header: 'Datum dodání', valueGetter: r => r.deliveryDate ? new Date(r.deliveryDate).toLocaleDateString() : '' },
  { key: 'totalGrossAmount', header: 'Částka s DPH',  valueGetter: r => r.totalGrossAmount.toFixed(2) },
  { key: 'totalVatAmount', header: 'DPH', valueGetter: r => r.totalVatAmount.toFixed(2) },
  { key: 'totalNetAmount', header: 'Částka bez DPH', valueGetter: r => r.totalNetAmount.toFixed(2) },
  { key: 'paymentTerms',   header: 'Platební podmínky', valueGetter: r => r.paymentTerms ?? ''},
  { key: 'vatRate',        header: 'Sazba DPH (%)',    valueGetter: r =>  VatRateLabels[r.vatRate as VatRate] },
  { key: 'variableSymbol', header: 'Variabilní symbol', valueGetter: r => r.variableSymbol ?? ''},
  { key: 'constantSymbol', header: 'Konstantní symbol', valueGetter: r => r.constantSymbol ?? ''},
  { key: 'specificSymbol', header: 'Specifický symbol', valueGetter: r => r.specificSymbol ?? '' },
];
