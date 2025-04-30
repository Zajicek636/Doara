import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

export interface QueryParams {
  params: Map<string, any>;
}

export interface CrudSettings<TId, TEntity> {
  baseUrl: string;
  postUrl?: string;
  queryParam?: string;

  postSuffix?: string;
  putSuffix?: string;
  getAllSuffix?: string;
  getSuffix?: string;

  mapper: (id: TId) => QueryParams;
  idGetter: (entity: TEntity) => TId;
}

export interface PagedList<T> {
  items: T[];
  totalCount: number;
}

export interface PagedRequest {
  skipCount: number;
  maxResultCount: number;
  [key: string]: any;
}

export class BaseCrud<TId, TDto, TCreateDto, TEditDto> {
  constructor(
    protected client: HttpClient,
    protected settings: CrudSettings<TId, TDto>
  ) {}

  private toQueryString(q: QueryParams): string {
    if (q.params.size === 0) return '';
    let qString = '?';
    q.params.forEach((value, key) => {
      if (value == null) return;
      if (qString !== '?') qString += '&';
      qString += `${key}=${value}`;
    });
    return qString;
  }

  public async get(id: TId,opts?: { useSuffix?: boolean }): Promise<TDto> {
    let url = this.settings.baseUrl;
    if (opts?.useSuffix ?? true) {
      if (this.settings.getSuffix) {
        url += `/${this.settings.getSuffix}`;
      }
    }
    const qString = this.toQueryString(this.settings.mapper(id));
    const res$ = this.client.get<TDto>(`${url}${qString}`);
    return await lastValueFrom(res$);
  }

  public async getAll( opts?: { useSuffix?: boolean }): Promise<PagedList<TDto>> {
    let url = this.settings.baseUrl;
    if (opts?.useSuffix ?? true) {
      if (this.settings.getAllSuffix) {
        url += `/${this.settings.getAllSuffix}`;
      }
    }
    const res$ = this.client.get<PagedList<TDto>>(`${url}`);
    return await lastValueFrom(res$);
  }

  public async post(id: TId, body: TCreateDto,  opts?: { useSuffix?: boolean }): Promise<TDto> {
    let url = this.settings.postUrl ?? this.settings.baseUrl;
    if (opts?.useSuffix ?? true) {
      if (this.settings.postSuffix) {
        url += `/${this.settings.postSuffix}`;
      }
    }
    const res$ = this.client.post<TDto>(`${url}${this.settings.queryParam ? '?' + this.settings.queryParam : ''}${id ? '=' + id : ''}`, body);
    return await lastValueFrom(res$);
  }

  public async put(id: TId, body: TEditDto, opts?: { useSuffix?: boolean }): Promise<TDto> {
    let url = this.settings.baseUrl;
    if (opts?.useSuffix ?? true) {
      if (this.settings.putSuffix) {
        url += `/${this.settings.putSuffix}`;
      }
    }
    const qString = this.toQueryString(this.settings.mapper(id));
    const res$ = this.client.put<TDto>(`${url}${qString}`, body);
    return await lastValueFrom(res$);
  }

  public async delete(id: TId): Promise<void> {
    const qString = this.toQueryString(this.settings.mapper(id));
    await lastValueFrom(this.client.delete(`${this.settings.baseUrl}${qString}`));
  }
  public async getPagedRequestAsync(params: PagedRequest):Promise<PagedList<TDto>> {
    const queryParams = new URLSearchParams();
    if (params.skipCount != null) queryParams.set('skipCount', params.skipCount.toString());
    if (params.maxResultCount != null) queryParams.set('maxResultCount', params.maxResultCount.toString());

    Object.entries(params).forEach(([key, value]) => {
      if (!['sorting', 'skipCount', 'maxResultCount'].includes(key) && value != null) {
        queryParams.set(key, value);
      }
    });

    const url = `${this.settings.baseUrl}${this.settings.getAllSuffix ? '/' + this.settings.getAllSuffix : ''}${queryParams.toString()}`;
    const res$ = this.client.get<PagedList<TDto>>(url);
    return await lastValueFrom(res$);
  }
}
