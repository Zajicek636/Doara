import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {NovaFakturaCrudSettings} from './nova-faktura-crud.settings';
import {InvoiceDetailDto, InvoiceDto} from './nova-faktura.interfaces';
import {AddressDetailDto, CountryDto} from '../../subjekty/data/subjekty.interfaces';

@Injectable({
  providedIn: 'root',
})
export class NovaFakturaDataService extends BaseCrud<string, InvoiceDetailDto, InvoiceDto, InvoiceDto> {
  constructor(client: HttpClient, settings: NovaFakturaCrudSettings) {
    super(client, settings);
  }
/*
  override async get(id: string, opts?: { useSuffix?: boolean }): Promise<InvoiceDetailDto> {
    const mock: InvoiceDetailDto = {
      id: "10",
      invoiceNumber: 'F2025EDIT',
      supplier: {
        id: "c8a067e2-b5b4-ec0e-5e89-3a19be4d7ece",
        name: `Tomáš`,
        ic: ``,
        dic: ``,
        isVatPayer: true,
        address: {
          id: `AddressId${1}`,
          street: `Ulice ${1}`,
          city: `Město ${1}`,
          postalCode: `${10000}`,
          country: {
            id: 'ID',
            name: 'Česká republika',
            code: 'CZ',
          } as CountryDto
        } as AddressDetailDto
      },
      customer: {
        id: `11603f2c-5adb-3ac3-1a00-3a19be4d400f`,
        name: `Jana`,
        ic: `10000000`,
        dic: `CZ${400000001}`,
        isVatPayer: false,
        address: {
          id: `AddressId${2}`,
          street: `Ulice ${2}`,
          city: `Město ${2}`,
          postalCode: `${10002}`,
          country: {
            id: 'IDD',
            name: 'Slovenski',
            code: 'SK',
          } as CountryDto
        } as AddressDetailDto
      },
      issueDate:    '2025-05-01T00:00:00.000Z',
      taxDate:      '2025-05-02T00:00:00.000Z',
      deliveryDate: '2025-05-03T00:00:00.000Z',
      totalNetAmount: 1234,
      totalVatAmount: 234,
      totalGrossAmount: 1468,
      paymentTerms:'30 dní',
      vatRate: 67,
      variableSymbol: '2025001',
      constantSymbol: '0308',
      specificSymbol: '001',
      items: [
        {
          id: 'item1',
          description: 'Položka 1',
          quantity: 2,
          unitPrice: 500,
          netAmount: 1000,
          vatRate: 67,
          vatAmount: 210,
          grossAmount: 1210
        },
        {
          id: 'item2',
          description: 'Položka 2',
          quantity: 1,
          unitPrice: 800,
          netAmount: 800,
          vatRate: 67,
          vatAmount: 168,
          grossAmount: 968
        }
      ]
    };
    return mock
  }*/
}
