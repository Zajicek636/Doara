import {Injectable} from '@angular/core';
import {CrudSettings, QueryParams} from '../../../shared/crud/base-crud-service';
import {ContainerDto} from "../../sklady-polozky/data/sklady-polozky.interfaces";
import {ContainerItemDto} from "./polozka-kontejneru.interfaces";

@Injectable({
  providedIn: 'root',
})
export class PolozkaKontejneruCrudSettings implements CrudSettings<string, ContainerItemDto> {
  baseUrl: string = 'api/Sklady/ContainerItem';
  postUrl?: string;
  queryParam?: string;
  getAllSuffix: string = "GetAll"

  idGetter(entity: ContainerItemDto): string {
    return entity.id;
  }

  mapper(id: string): QueryParams {
    const dict = new Map<string, string>();
    dict.set('id', id);
    return { params: dict };
  }
}
