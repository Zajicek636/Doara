
import {ColumnSetting} from '../../../shared/table/table/table.settings';
import {InvoiceDto, VatRate, VatRateLabels} from '../../nova-faktura/data/nova-faktura.interfaces';

export const INVOICE_COLUMNS: ColumnSetting<InvoiceDto>[] = [
  { key: 'invoiceNumber', header: 'Číslo faktury', valueGetter: r => r.invoiceNumber },
  { key: 'issueDate',      header: 'Datum vystavení', valueGetter: r => new Date(r.issueDate).toLocaleDateString() },
  { key: 'taxDate',        header: 'Datum DPH',       valueGetter: r => new Date(r.taxDate).toLocaleDateString() },
  { key: 'deliveryDate',   header: 'Datum dodání',     valueGetter: r => new Date(r.deliveryDate).toLocaleDateString() },
  { key: 'totalNetAmount', header: 'Celkem netto',    valueGetter: r => r.totalNetAmount.toFixed(2) },
  { key: 'totalVatAmount', header: 'DPH',              valueGetter: r => r.totalVatAmount.toFixed(2) },
  { key: 'totalGrossAmount', header: 'Celkem brutto',  valueGetter: r => r.totalGrossAmount.toFixed(2) },
  { key: 'paymentTerms',   header: 'Platební podmínky', valueGetter: r => r.paymentTerms },
  { key: 'vatRate',        header: 'Sazba DPH (%)',    valueGetter: r =>  VatRateLabels[r.vatRate as VatRate] },
  { key: 'variableSymbol', header: 'Variabilní symbol', valueGetter: r => r.variableSymbol },
  { key: 'constantSymbol', header: 'Konstantní symbol', valueGetter: r => r.constantSymbol },
  { key: 'specificSymbol', header: 'Specifický symbol', valueGetter: r => r.specificSymbol },
];
