import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {InvoiceCreateEditDto} from './nova-faktura.interfaces';

@Injectable({
  providedIn: 'root',
})
export class NovaFakturaCrudSettings implements CrudSettings<string, InvoiceCreateEditDto> {
  baseUrl: string = '/api/Subjekty/Subjekty';
  postUrl?: string;
  queryParam?: string;

  idGetter(entity: any): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, any>();
    return { params: dict };
  }
}
