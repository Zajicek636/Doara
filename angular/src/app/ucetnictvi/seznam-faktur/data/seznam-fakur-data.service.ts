import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {SeznamFakturCrudSettings} from './seznam-faktur-crud.settings';
import {InvoiceDto} from '../../nova-faktura/data/nova-faktura.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SeznamFakurDataService extends BaseCrud<string, InvoiceDto, InvoiceDto, InvoiceDto> {
  constructor(client: HttpClient, settings: SeznamFakturCrudSettings) {
    super(client, settings);
  }

  override async getPagedRequestAsync(params: PagedRequest): Promise<PagedList<InvoiceDto>> {
    // mock 1000 invoices
    const invoices: InvoiceDto[] = Array.from({ length: 1000 }, (_, i) => ({
      id: `INV-${i+1}`,
      invoiceNumber: `2025/00${i+1}`,
      supplierId: `SUP-${(i%10)+1}`,
      customerId: `CUST-${(i%20)+1}`,
      issueDate: new Date(2025, 0, (i%30)+1).toISOString(),
      taxDate:   new Date(2025, 1, (i%28)+1).toISOString(),
      deliveryDate: new Date(2025, 2, (i%31)+1).toISOString(),
      totalNetAmount: 100 + i,
      totalVatAmount: (100 + i) * 0.21,
      totalGrossAmount: (100 + i) * 1.21,
      paymentTerms: '14 days',
      vatRate: 21,
      variableSymbol: `${100000 + i}`,
      constantSymbol: '0308',
      specificSymbol: `${200000 + i}`
    }));

    const skip = params.skipCount ?? 0;
    const take = params.maxResultCount ?? 10;
    const items = invoices.slice(skip, skip + take);
    return { items, totalCount: invoices.length };
  }
}

