import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {SeznamFakturDto} from '../../seznam-faktur/data/seznam-faktur.interfaces';
import {SubjektyDto} from './subjekty.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SubjektyCrudSettings implements CrudSettings<string, SubjektyDto> {
  baseUrl: string = 'https://example.com/api/Subjekty/Subjekty';
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
