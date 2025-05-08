import {Injectable} from '@angular/core';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {NovaFakturaCrudSettings} from './nova-faktura-crud.settings';
import {InvoiceDetailDto, InvoiceDto} from './nova-faktura.interfaces';
import {AddressDetailDto, CountryDto} from '../../subjekty/data/subjekty.interfaces';

@Injectable({
  providedIn: 'root',
})
export class NovaFakturaDataService extends BaseCrud<string, InvoiceDetailDto, InvoiceDto, InvoiceDto> {
  constructor(client: HttpClient, settings: NovaFakturaCrudSettings) {
    super(client, settings);
  }
}
