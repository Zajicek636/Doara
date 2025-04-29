import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {AddressDetailDto, CountryDto, SubjektDetailDto} from './subjekty.interfaces';
import {SubjektyCrudSettings} from './subjekty-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class SubjektyDataService extends BaseCrud<string, SubjektDetailDto, SubjektDetailDto, SubjektDetailDto> {
  constructor(client: HttpClient, settings: SubjektyCrudSettings) {
    super(client, settings);
  }

  public override async getPagedRequestAsync(params: PagedRequest): Promise<PagedList<SubjektDetailDto>> {
    // 1) Generování všech subjektů
    const allSubjekty: SubjektDetailDto[] = Array.from({ length: 500 }, (_, i) => ({
      id: `SubId${i + 1}`,
      Name: `Jméno Subjektu ${i + 1}`,
      Ic: `${10000000 + i}`,
      Dic: `CZ${400000000 + i}`,
      IsVatPayer: i % 2 === 0,
      AddressDetailDto: {
        id: `AddressId${i + 1}`,
        Street: `Ulice ${i + 1}`,
        City: `Město ${i + 1}`,
        PostalCode: `${10000 + i}`,
        CountryDto: {
          id: `CountryId${i % 3 + 1}`,
          Name: ['Česká republika', 'Slovensko', 'Polsko'][i % 3],
          Code: ['CZ', 'SK', 'PL'][i % 3],
        } as CountryDto
      } as AddressDetailDto
    }));

    // 2) Vytažení parametrů stránkování
    const skip  = params.skipCount  ?? 0;
    const take  = params.maxResultCount ?? 10;

    // 3) Sestavení stránky
    const pageItems = allSubjekty.slice(skip, skip + take);

    // 4) Návrat paged listu
    return {
      items:      pageItems,
      totalCount: allSubjekty.length
    };
  }

}
