import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';

@Injectable({
  providedIn: 'root',
})
export class SkladyPohybyPolozkyCrudSettings implements CrudSettings<string, any> {
  baseUrl: string = '/api/Sklady/ContainerItem';
  addStock = "AddStock";
  removeMovement = "RemoveMovement";
  reserve = "Reserve";
  use = "Use";
  reservationToUsage = "ReservationToUsage";
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
