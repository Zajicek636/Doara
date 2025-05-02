import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {NovaFakturaCrudSettings} from './nova-faktura-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class NovaFakturaDataService extends BaseCrud<string, any, any, any> {
  constructor(client: HttpClient, settings: NovaFakturaCrudSettings) {
    super(client, settings);
  }

  public async getPagedRequest(params: any): Promise<any[]> {
    const b: any[] = []
    for (let i = 0; i < 1; i++) {
      const a: any = {
        id: `SubId${i}`,
        Name: `JmenoSub${i+i}*`,
        Ic: `11370105`,
        Dic: `CZ410605060`,
        IsVatPayer: false,
        AddressDetailDto: {
          id: `AdderssId${i}`,
          Street: `Street${i}`,
          City: `City${i}`,
          PostalCode: `${i+1*123}`,
          CountryDto: {
            id: `CountryId${i}`,
            Name:`CZ`,
            Code:`CZ`,
          }
        }
      }
      b.push(a)
    }
    return b
  }

  async getOptionsForSubject() {
    //await this.get();
  }

  async getInvoiceItems() {
  }

}
