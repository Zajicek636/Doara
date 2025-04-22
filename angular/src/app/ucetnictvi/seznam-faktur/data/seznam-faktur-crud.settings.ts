import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {SeznamFakturDto} from './seznam-faktur.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SeznamFakturCrudSettings implements CrudSettings<string, SeznamFakturDto> {
  baseUrl: string = 'https://example.com/api/SeznamFaktur/SeznamFakturAll';
  postUrl?: string;
  queryParam?: string;

  idGetter(entity: any): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, string>();
    dict.set('id', id);
    return { params: dict };
  }
}
