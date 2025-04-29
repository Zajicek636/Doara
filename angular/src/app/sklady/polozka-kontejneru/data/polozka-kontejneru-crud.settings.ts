import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';

@Injectable({
  providedIn: 'root',
})
export class PolozkaKontejneruCrudSettings implements CrudSettings<string, any> {
  baseUrl: string = 'https://example.com/api/SkladyPolozky/SkladyPolozky';
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
