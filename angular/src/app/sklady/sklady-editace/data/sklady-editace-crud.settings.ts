import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {SkladyEditaceDto} from './sklady-editace.interfaces';
@Injectable({
  providedIn: 'root',
})
export class SkladyEditaceCrudSettings implements CrudSettings<string, SkladyEditaceDto> {
  baseUrl: string = 'https://example.com/api/SkladyEditace/SkladyEditace';
  postUrl?: string;
  queryParam?: string;

  idGetter(entity: SkladyEditaceDto): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, string>();
    dict.set('id', id);
    return { params: dict };
  }
}
