import {Injectable} from '@angular/core';
import {BaseCrud, PagedList} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {PolozkyFakturyCrudSettings} from './polozky-faktury-crud.settings';
import {InvoiceItemDto} from './polozky-faktury.interfaces';

@Injectable({
  providedIn: 'root',
})
export class PolozkyFakturyDataService extends BaseCrud<string, InvoiceItemDto, InvoiceItemDto, any> {
  constructor(client: HttpClient, settings: PolozkyFakturyCrudSettings) {
    super(client, settings);
  }

  public override async getAll(id?: string, opts?: { useSuffix?: boolean }): Promise<PagedList<InvoiceItemDto>> {
    const mockItems: InvoiceItemDto[] = [
      {
        id: 'item1',
        invoiceId: 'inv1',
        description: 'Položka 1',
        quantity: 2,
        unitPrice: 500,
        netAmount: 1000,
        vatRate: 21,
        vatAmount: 210,
        grossAmount: 1210
      },
      {
        id: 'item2',
        invoiceId: 'inv1',
        description: 'Položka 2',
        quantity: 1,
        unitPrice: 800,
        netAmount: 800,
        vatRate: 21,
        vatAmount: 168,
        grossAmount: 968
      }
    ];

    return {
      items: mockItems,
      totalCount: mockItems.length,
    };
  }

}
