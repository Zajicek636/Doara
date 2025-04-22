import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SkladyEditaceCrudSettings} from './sklady-editace-crud.settings';
import {lastValueFrom} from 'rxjs';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {SkladyEditaceDto} from './sklady-editace.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SkladyEditaceDataService extends BaseCrud<string, SkladyEditaceDto, SkladyEditaceDto, SkladyEditaceDto> {
  constructor(client: HttpClient, settings: SkladyEditaceCrudSettings) {
    super(client, settings);
  }

  public async getPagedRequest(params: any): Promise<SkladyEditaceDto[]> {
    const res$ = this.client.get<SkladyEditaceDto[]>(`${this.settings.baseUrl}`, { params });
    return await lastValueFrom(res$);
  }

}
