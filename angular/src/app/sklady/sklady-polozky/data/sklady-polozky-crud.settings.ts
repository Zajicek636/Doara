import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {SkladyPolozkyDto} from './sklady-polozky.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SkladyPolozkyCrudSettings implements CrudSettings<string, SkladyPolozkyDto> {
  baseUrl: string = 'https://example.com/api/SkladyPolozky/SkladyPolozky';
  postUrl?: string;
  queryParam?: string;

  idGetter(entity: SkladyPolozkyDto): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, string>();
    dict.set('id', id);
    return { params: dict };
  }
}
