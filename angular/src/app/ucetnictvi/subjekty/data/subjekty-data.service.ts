import {Injectable} from '@angular/core';
import {BaseCrud, PagedList, PagedRequest} from '../../../shared/crud/base-crud-service';
import {HttpClient} from '@angular/common/http';
import {
  AddressDetailDto,
  CountryDto,
  SubjektCreateEditDto,
  SubjektDetailDto
} from './subjekty.interfaces';
import {SubjektyCrudSettings} from './subjekty-crud.settings';

@Injectable({
  providedIn: 'root',
})
export class SubjektyDataService extends BaseCrud<string, SubjektDetailDto, SubjektCreateEditDto, SubjektCreateEditDto> {
  constructor(client: HttpClient, settings: SubjektyCrudSettings) {
    super(client, settings);
  }

  public override async getPagedRequestAsync(params: PagedRequest): Promise<PagedList<SubjektDetailDto>> {
    // 1) Generování všech subjektů
    const allSubjekty: SubjektDetailDto[] = Array.from({ length: 500 }, (_, i) => ({
      id: `SubId${i + 1}`,
      name: `Jméno Subjektu ${i + 1}`,
      ic: `${10000000 + i}`,
      dic: `CZ${400000000 + i}`,
      isVatPayer: i % 2 === 0,
      address: {
        id: `AddressId${i + 1}`,
        street: `Ulice ${i + 1}`,
        city: `Město ${i + 1}`,
        postalCode: `${10000 + i}`,
        countryDto: {
          id: ['ID', 'IDD'][i % 2],
          name: ['Česká republika', 'Slovensko'][i % 2],
          code: ['CZ', 'SK'][i % 2],
        } as CountryDto
      } as AddressDetailDto
    }));
    const skip  = params.skipCount  ?? 0;
    const take  = params.maxResultCount ?? 10;
    const pageItems = allSubjekty.slice(skip, skip + take);
    return {
      items:      pageItems,
      totalCount: allSubjekty.length
    };
  }

  public override async getAll(opts?: { useSuffix?: boolean }): Promise<PagedList<SubjektDetailDto>> {
    const allSubjekty: SubjektDetailDto[] = Array.from({ length: 500 }, (_, i) => ({
      id: `SubId${i + 1}`,
      name: `Jméno Subjektu ${i + 1}`,
      ic: `${10000000 + i}`,
      dic: `CZ${400000000 + i}`,
      isVatPayer: i % 2 === 0,
      address: {
        id: `AddressId${i + 1}`,
        street: `Ulice ${i + 1}`,
        city: `Město ${i + 1}`,
        postalCode: `${10000 + i}`,
        countryDto: {
          id: `CountryId${i % 3 + 1}`,
          name: ['Česká republika', 'Slovensko'][i % 2],
          code: ['CZ', 'SK'][i % 3],
        } as CountryDto
      } as AddressDetailDto
    }));
    return {
      items: allSubjekty,
      totalCount: allSubjekty.length
    }
  }

}
