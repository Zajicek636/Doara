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
  protected origin: string = "https://localhost:44346";

  constructor(
    protected client: HttpClient,
    protected settings: CrudSettings<TId, TDto>
  ) {

  }

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

  private buildUrl(suffix?: string, query?: string): string {
    return `${this.origin}${this.settings.baseUrl}${suffix ?? ''}${query ?? ''}`;
  }

  public async get(id: TId, opts?: { useSuffix?: boolean }): Promise<TDto> {
    let suffix = '';
    if (opts?.useSuffix ?? true) {
      suffix = this.settings.getSuffix ? `/${this.settings.getSuffix}` : '';
    }
    const qString = this.toQueryString(this.settings.mapper(id));
    const res$ = this.client.get<TDto>(this.buildUrl(suffix, qString));
    return await lastValueFrom(res$);
  }

  public async getAll(id?: TId, opts?: { useSuffix?: boolean }): Promise<PagedList<TDto>> {
    let suffix = '';
    if (id != null) {
      suffix += `/${id}`;
    }
    if (opts?.useSuffix ?? true) {
      suffix += this.settings.getAllSuffix ? `/${this.settings.getAllSuffix}` : '';
    }
    const route = this.buildUrl(suffix)
    const res$ = this.client.get<PagedList<TDto>>(route);
    return await lastValueFrom(res$);
  }

  public async post(id: TId, body: TCreateDto, opts?: { useSuffix?: boolean }): Promise<TDto> {
    let suffix = '';
    if (opts?.useSuffix ?? this.settings.postSuffix) {
      suffix += `/${this.settings.postSuffix}`;
    }
    const query = this.settings.queryParam ? `?${this.settings.queryParam}${id ? '=' + id : ''}` : '';
    const res$ = this.client.post<TDto>(this.buildUrl(suffix, query), body);
    return await lastValueFrom(res$);
  }

  public async put(id: TId, body: TEditDto, opts?: { useSuffix?: boolean }): Promise<TDto> {
    let suffix = '';
    if (opts?.useSuffix ?? this.settings.putSuffix) {
      suffix = `/${this.settings.putSuffix}`;
    }
    const qString = this.toQueryString(this.settings.mapper(id));
    const res$ = this.client.put<TDto>(this.buildUrl(suffix, qString), body);
    return await lastValueFrom(res$);
  }

  public async delete(id: TId): Promise<void> {
    const qString = this.toQueryString(this.settings.mapper(id));
    await lastValueFrom(this.client.delete(this.buildUrl('', qString)));
  }

  public async getPagedRequestAsync(params: PagedRequest, id?: TId): Promise<PagedList<TDto>> {
    const queryParams = new URLSearchParams();
    if (params.skipCount != null) queryParams.set('skipCount', params.skipCount.toString());
    if (params.maxResultCount != null) queryParams.set('maxResultCount', params.maxResultCount.toString());

    Object.entries(params).forEach(([key, value]) => {
      if (!['sorting', 'skipCount', 'maxResultCount'].includes(key) && value != null) {
        queryParams.set(key, value);
      }
    });

    let suffix = '';
    if (id != null) suffix += `/${id}`;
    if (this.settings.getAllSuffix) suffix += `/${this.settings.getAllSuffix}`;
    const query = queryParams.toString() ? `?${queryParams.toString()}` : '';
    const res$ = this.client.get<PagedList<TDto>>(this.buildUrl(suffix, query));
    return await lastValueFrom(res$);
  }
}
