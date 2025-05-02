import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {InvoiceItemDto} from './polozky-faktury.interfaces';

@Injectable({
  providedIn: 'root',
})
export class PolozkyFakturyCrudSettings implements CrudSettings<string, InvoiceItemDto> {
  baseUrl: string = 'api/Ucetnictvi/InvoiceItem';
  postUrl?: string;
  queryParam?: string;

  postSuffix?: string = "ManageMany";
  getAllSuffix: string = "GetAll"

  idGetter(entity: InvoiceItemDto): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, string>();
    dict.set('id', id);
    return { params: dict };
  }
}
