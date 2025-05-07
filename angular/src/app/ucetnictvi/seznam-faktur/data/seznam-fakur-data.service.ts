import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {SeznamFakturCrudSettings} from './seznam-faktur-crud.settings';
import {InvoiceDto} from '../../nova-faktura/data/nova-faktura.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SeznamFakurDataService extends BaseCrud<string, InvoiceDto, InvoiceDto, InvoiceDto> {
  constructor(client: HttpClient, settings: SeznamFakturCrudSettings) {
    super(client, settings);
  }
}

