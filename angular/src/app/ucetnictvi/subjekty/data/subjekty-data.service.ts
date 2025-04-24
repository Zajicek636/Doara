import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {SubjektDetailDto} from './subjekty.interfaces';
import {SubjektyCrudSettings} from './subjekty-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class SubjektyDataService extends BaseCrud<string, SubjektDetailDto, SubjektDetailDto, SubjektDetailDto> {
  constructor(client: HttpClient, settings: SubjektyCrudSettings) {
    super(client, settings);
  }

  public async getPagedRequest(params: any): Promise<SubjektDetailDto[]> {
    const b: any[] = []
    for (let i = 0; i < 1; i++) {
      const a: SubjektDetailDto = {
        id: `SubId${i}`,
        Name: `JmenoSub${i+i}*`,
        Ic: `1${i + 13}7${i * 6}1${i * 3}4${i*3}`,
        Dic: `4${i + 10}6${i * 3}5${i * 3}6${i*2}`,
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

    /* const res$ = this.client.get<SeznamFakturDto[]>(`${this.settings.baseUrl}`, { params });
     return await lastValueFrom(res$);*/
  }

}
