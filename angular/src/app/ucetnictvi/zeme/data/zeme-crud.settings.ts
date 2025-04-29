import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {CountryDto} from '../../subjekty/data/subjekty.interfaces';

@Injectable({
  providedIn: 'root',
})
export class ZemeCrudSettings implements CrudSettings<string, CountryDto> {
  baseUrl: string = 'api/Ucetnictvi/Country';
  postUrl?: string;
  queryParam?: string;

  idGetter(entity: any): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, any>();
    dict.set('id', id);
    return { params: dict };
  }
}
