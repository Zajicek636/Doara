import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {SeznamFakturDto} from '../../seznam-faktur/data/seznam-faktur.interfaces';
import {HttpClient} from '@angular/common/http';
import {SkladyPolozkyCrudSettings} from '../../../sklady/sklady-polozky/data/sklady-polozky-crud.settings';
import {SubjektyDto} from './subjekty.interfaces';
import {SubjektyCrudSettings} from './subjekty-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class SubjektyDataService extends BaseCrud<string, SubjektyDto, SubjektyDto, SubjektyDto> {
  constructor(client: HttpClient, settings: SubjektyCrudSettings) {
    super(client, settings);
  }

  public async getPagedRequest(params: any): Promise<SubjektyDto[]> {
    const b: any[] = []
    for (let i = 0; i < 100; i++) {
      const a: SubjektyDto = {
        id: `SubId${i}`,
        jmeno: `JmenoSub${i+i}*`,
        prijmeni: `PrijmeniSub${i*i}`,
        ico: `4${i + 10}6${i * 3}5${i * 3}6${i*2}`
      }
      b.push(a)
    }
    return b

    /* const res$ = this.client.get<SeznamFakturDto[]>(`${this.settings.baseUrl}`, { params });
     return await lastValueFrom(res$);*/
  }

}
