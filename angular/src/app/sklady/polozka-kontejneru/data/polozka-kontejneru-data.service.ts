import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {PolozkaKontejneruCrudSettings} from './polozka-kontejneru-crud.settings';
import {
  ContainerItemCreateEditDto,
  ContainerItemDto,
  ContainerItemState,
  QuantityType
} from "./polozka-kontejneru.interfaces";
import {lastValueFrom} from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class PolozkaKontejneruDataService extends BaseCrud<string, ContainerItemDto, ContainerItemCreateEditDto, ContainerItemCreateEditDto> {
  constructor(client: HttpClient, settings: PolozkaKontejneruCrudSettings) {
    super(client, settings);
  }

  async getAllWithDetail(extraParams: Record<string, any> = {}): Promise<PagedList<ContainerItemDto>> {
    const suffix = '/GetAllWithDetail';
    const query = this.buildQueryParams({}, extraParams);
    const res$ = this.client.get<PagedList<ContainerItemDto>>(this.buildUrl(suffix, query));
    return await lastValueFrom(res$);
  }
}
