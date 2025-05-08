import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {InvoiceDto} from '../../nova-faktura/data/nova-faktura.interfaces';



@Injectable({
  providedIn: 'root',
})
export class SeznamFakturCrudSettings implements CrudSettings<string, InvoiceDto> {
  baseUrl: string = '/api/Ucetnictvi/Invoice';
  postUrl?: string;
  getAllSuffix?: string = "GetAll";
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
