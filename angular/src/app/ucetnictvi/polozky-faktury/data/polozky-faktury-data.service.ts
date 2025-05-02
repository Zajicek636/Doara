import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {PolozkyFakturyCrudSettings} from './polozky-faktury-crud.settings';
import {InvoiceItemCreateDto, InvoiceItemDto} from './polozky-faktury.interfaces';

@Injectable({
  providedIn: 'root',
})
export class PolozkyFakturyDataService extends BaseCrud<string, InvoiceItemDto, InvoiceItemCreateDto, any> {
  constructor(client: HttpClient, settings: PolozkyFakturyCrudSettings) {
    super(client, settings);
  }

}
