import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {
  AddressDetailDto,
  CountryDto,
  SubjektCreateEditDto,
  SubjektDetailDto
} from './subjekty.interfaces';
import {SubjektyCrudSettings} from './subjekty-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class SubjektyDataService extends BaseCrud<string, SubjektDetailDto, SubjektCreateEditDto, SubjektCreateEditDto> {
  constructor(client: HttpClient, settings: SubjektyCrudSettings) {
    super(client, settings);
  }
}
