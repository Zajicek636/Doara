import {Injectable} from '@angular/core';
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
  public override async getAll(id?: string, opts?: { useSuffix?: boolean }): Promise<PagedList<ContainerDto>> {
    return {
      items: [
        {
          id: "1",
          name: 'Container 1',
          description: 'This is the first container',
        },
        {
          id: "2",
          name: 'Container 2',
          description: 'This is the second container',
        },
        {
          id: "3",
          name: 'Container 3',
          description: 'This is the third container',
        }
      ],
      totalCount: 3,
    }
  }

}

