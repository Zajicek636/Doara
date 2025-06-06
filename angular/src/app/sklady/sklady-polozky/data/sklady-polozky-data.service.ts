﻿import {Injectable} from '@angular/core';
import {SkladyPolozkyCrudSettings} from './sklady-polozky-crud.settings';
import {HttpClient} from '@angular/common/http';
import {BaseCrud, PagedList} from '../../../shared/crud/base-crud-service';
import {ContainerDto} from './sklady-polozky.interfaces';

@Injectable({
  providedIn: 'root',
})
export class SkladyPolozkyDataService extends BaseCrud<string, ContainerDto, ContainerDto, ContainerDto> {
  constructor(client: HttpClient, settings: SkladyPolozkyCrudSettings) {
    super(client, settings);
  }

}

