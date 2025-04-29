import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {SkladyPolozkyCrudSettings} from '../../../sklady/sklady-polozky/data/sklady-polozky-crud.settings';
import {SeznamFakturDto} from './seznam-faktur.interfaces';
import {lastValueFrom} from 'rxjs';
import {SeznamFakturCrudSettings} from './seznam-faktur-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class SeznamFakurDataService extends BaseCrud<string, SeznamFakturDto, SeznamFakturDto, SeznamFakturDto> {
  constructor(client: HttpClient, settings: SeznamFakturCrudSettings) {
    super(client, settings);
  }

  public override async getPagedRequestAsync(params: PagedRequest): Promise<PagedList<SeznamFakturDto>> {
    // Vygenerujeme všech 1000 "faktur"
    const allFaktury: SeznamFakturDto[] = Array.from({ length: 1000 }, (_, i) => ({
      id:             `FakId${i + 1}`,
      subjektname:    `Subjekt ${i + 1}`,
      subjektIco:     `ICO${100000 + i}`,
    }));

    // Vypočítáme offset a limit
    const skip  = params.skipCount ?? 0;
    const take  = params.maxResultCount ?? 10;

    // Vybereme jen požadovanou "stránku"
    const pageItems = allFaktury.slice(skip, skip + take);

    return {
      items:      pageItems,
      totalCount: allFaktury.length
    };
  }

}
