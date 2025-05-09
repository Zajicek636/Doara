import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {lastValueFrom} from 'rxjs';
import {BaseCrud} from '../../../shared/crud/base-crud-service';
import {SkladyPohybyPolozkyCrudSettings} from './sklady-pohyby-polozky-crud.settings';
import {ContainerDto} from '../../sklady-polozky/data/sklady-polozky.interfaces';
import {ContainerItemDto} from '../../polozka-kontejneru/data/polozka-kontejneru.interfaces';
import {MovementDto} from './sklady-pohyby-polozky.interfaces';
@Injectable({
  providedIn: 'root',
})
export class SkladyPohybyPolozkyDataService extends BaseCrud<string, any, any, any> {
  constructor(client: HttpClient, settings: SkladyPohybyPolozkyCrudSettings) {
    super(client, settings);
  }

  async postBySuffix(
    body: MovementDto = {} as MovementDto,
    suffix: 'AddStock' | 'RemoveMovement' | 'Reserve' | 'Use' | 'ReservationToUsage',
    id?: string,
    stockMovementId?: string,
  ): Promise<any> {
    const queryParams: string[] = [];
    if (id) queryParams.push(`id=${encodeURIComponent(id)}`);
    if (stockMovementId) queryParams.push(`stockMovementId=${encodeURIComponent(stockMovementId)}`);

    const queryString = queryParams.length ? `?${queryParams.join('&')}` : '';
    const res$ = this.client.post(`${this.origin + this.settings.baseUrl}/${suffix}${queryString}`, body);
    return lastValueFrom(res$)
  }
}
