import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {CountryDto} from '../../subjekty/data/subjekty.interfaces';
import {ZemeCrudSettings} from './zeme-crud.settings';


@Injectable({
  providedIn: 'root',
})
export class ZemeDataService extends BaseCrud<string, CountryDto, CountryDto, CountryDto> {
  constructor(client: HttpClient, settings: ZemeCrudSettings) {
    super(client, settings);
  }


  override async getAll(): Promise<PagedList<CountryDto>> {
    return {items: [{name: "Česká republika", code: "CZ", id: "ID"}, {name: "Slovensko", code: "SK", id: "IDD"}], totalCount: 2}
  }
}
