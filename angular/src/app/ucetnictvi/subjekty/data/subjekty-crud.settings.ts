import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';

import {SubjektDetailDto} from './subjekty.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SubjektyCrudSettings implements CrudSettings<string, SubjektDetailDto> {
  baseUrl: string = '/api/Ucetnictvi/Subject';
  postUrl?: string;

  getAllSuffix: string = "GetAllWithDetail"
  postSuffix: string = "CreateWithAddress";
  putSuffix: string = "UpdateWithAddress";

  idGetter(entity: any): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, any>();
    dict.set('id', id);
    return { params: dict };
  }
}
