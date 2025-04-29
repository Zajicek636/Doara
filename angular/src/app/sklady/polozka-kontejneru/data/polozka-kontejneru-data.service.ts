import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {PolozkaKontejneruCrudSettings} from './polozka-kontejneru-crud.settings';
import {SubjektDetailDto} from '../../../ucetnictvi/subjekty/data/subjekty.interfaces';

@Injectable({
  providedIn: 'root',
})
export class PolozkaKontejneruDataService extends BaseCrud<string, any, any, any> {
  constructor(client: HttpClient, settings: PolozkaKontejneruCrudSettings) {
    super(client, settings);
  }

  public async getPagedRequest(params: any): Promise<SubjektDetailDto[]> {
    const b: any[] = []

    return b

    /* const res$ = this.client.get<SeznamFakturDto[]>(`${this.settings.baseUrl}`, { params });
     return await lastValueFrom(res$);*/
  }

}
