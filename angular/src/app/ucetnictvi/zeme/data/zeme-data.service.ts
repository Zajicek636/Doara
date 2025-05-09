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
  
}
