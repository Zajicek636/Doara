import {Injectable} from '@angular/core';
import {BaseCrud} from '../../shared/crud/base-crud-service';
import {SkladyPolozkyCrudSettings, SkladyPolozkyDto} from './sklady-polozky-crud-settings';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class SkladyPolozkyDataService extends BaseCrud<string, SkladyPolozkyDto, SkladyPolozkyDto, SkladyPolozkyDto> {
  constructor(client: HttpClient, settings: SkladyPolozkyCrudSettings) {
    super(client, settings);
  }

}
