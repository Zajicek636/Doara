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


@Injectable({
  providedIn: 'root',
})
export class PolozkaKontejneruDataService extends BaseCrud<string, ContainerItemDto, ContainerItemCreateEditDto, ContainerItemCreateEditDto> {
  constructor(client: HttpClient, settings: PolozkaKontejneruCrudSettings) {
    super(client, settings);
  }

  override async getPagedRequestAsync(params: PagedRequest): Promise<PagedList<ContainerItemDto>> {
    const allItems: ContainerItemDto[] = [];

    for (let i = 1; i <= 1000; i++) {
      allItems.push({
        id: `mock-id-${i}`,
        state: ContainerItemState.New,
        quantityType: QuantityType.Grams,
        name: `Mock Item ${i}`,
        description: `Description for mock item ${i}`,
        purchaseUrl: `https://example.com/item/${i}`,
        quantity: i * 10,
        realPrice: i * 5,
        presentationPrice: i * 6,
        markup: 10,
        markupRate: 20,
        discount: 0,
        discountRate: 0,
        containerId: `container-${i}`
      });
    }

    const { skipCount, maxResultCount } = params;
    const pagedItems = allItems.slice(skipCount, skipCount + maxResultCount);


    return {
      items: pagedItems,
      totalCount: allItems.length
    };
  }

}
