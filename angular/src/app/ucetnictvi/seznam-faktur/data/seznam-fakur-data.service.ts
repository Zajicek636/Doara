import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {SkladyPolozkyCrudSettings} from '../../../sklady/sklady-polozky/data/sklady-polozky-crud.settings';
import {SeznamFakturDto} from './seznam-faktur.interfaces';
import {lastValueFrom} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SeznamFakurDataService extends BaseCrud<string, SeznamFakturDto, SeznamFakturDto, SeznamFakturDto> {
  constructor(client: HttpClient, settings: SkladyPolozkyCrudSettings) {
    super(client, settings);
  }

  public async getPagedRequest(params: any): Promise<SeznamFakturDto[]> {
    const b: any[] = []
    for (let i = 0; i < 100; i++) {
      const a: SeznamFakturDto = {
        id: `13451${i}`,
        subjektname: `${i+i}*`,
        subjektIco: `${i*i}`,
      }
      b.push(a)
    }
    return b

    const res$ = this.client.get<SeznamFakturDto[]>(`${this.settings.baseUrl}`, { params });
    return await lastValueFrom(res$);
  }

}
