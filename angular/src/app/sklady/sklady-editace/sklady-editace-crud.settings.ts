import {CrudSettings, QueryParams} from '../../shared/crud/base-crud-service';
import {Injectable} from '@angular/core';

export interface SkladyEditaceDto {
  id: string;
}
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
