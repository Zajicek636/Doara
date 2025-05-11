import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {InvoiceCreateEditDto} from './nova-faktura.interfaces';

@Injectable({
  providedIn: 'root',
})
export class NovaFakturaCrudSettings implements CrudSettings<string, InvoiceCreateEditDto> {
  baseUrl: string = '/api/Ucetnictvi/Invoice';
  postUrl?: string;
  queryParam?: string;
  postSuffix?: string = "Complete";
  idGetter(entity: any): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, any>();
    dict.set('id', id);
    return { params: dict };
  }
}
