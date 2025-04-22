import {Injectable} from '@angular/core';
import {SkladyPolozkyCrudSettings} from './sklady-polozky-crud.settings';
import {HttpClient} from '@angular/common/http';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {SkladyPolozkyDto} from './sklady-polozky.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SkladyPolozkyDataService extends BaseCrud<string, SkladyPolozkyDto, SkladyPolozkyDto, SkladyPolozkyDto> {
  constructor(client: HttpClient, settings: SkladyPolozkyCrudSettings) {
    super(client, settings);
  }

}
