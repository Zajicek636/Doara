import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {ContainerDto} from './sklady-polozky.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SkladyPolozkyCrudSettings implements CrudSettings<string, ContainerDto> {
  baseUrl: string = '/api/Sklady/Container';
  postUrl?: string;
  queryParam?: string;
  getAllSuffix: string = "GetAll"

  useContainerIdParamKey: boolean = false;

  idGetter(entity: ContainerDto): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, string>();
    dict.set('id', id);
    return { params: dict };
  }
}
