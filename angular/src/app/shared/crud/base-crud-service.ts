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
  ) {}

  protected buildUrl(suffix?: string, query?: string): string {
    return `${this.origin}${this.settings.baseUrl}${suffix ?? ''}${query ?? ''}`;
  }

  protected buildQueryParams(...paramObjects: Record<string, any>[]): string {
    const queryParams = new URLSearchParams();

    paramObjects.forEach(params => {
      Object.entries(params || {}).forEach(([key, value]) => {
        if (value != null) {
          queryParams.set(key, value.toString());
        }
      });
    });

    return queryParams.toString() ? `?${queryParams.toString()}` : '';
  }

  public async get(id: TId, opts?: { useSuffix?: boolean, extraParams?: Record<string, any> }): Promise<TDto> {
    let suffix = '';
    if (opts?.useSuffix ?? true) {
      suffix = this.settings.getSuffix ? `/${this.settings.getSuffix}` : '';
    }

    const mappedParams = Object.fromEntries(this.settings.mapper(id).params);
    const query = this.buildQueryParams(mappedParams, opts?.extraParams ?? {});
    const res$ = this.client.get<TDto>(this.buildUrl(suffix, query));
    return await lastValueFrom(res$);
  }

  public async getAll(id?: TId, opts?: { useSuffix?: boolean, extraParams?: Record<string, any> }): Promise<PagedList<TDto>> {
    let suffix = '';
    if (id != null) suffix += `/${id}`;
    if (opts?.useSuffix ?? true) suffix += this.settings.getAllSuffix ? `/${this.settings.getAllSuffix}` : '';

    const mappedParams = id != null ? Object.fromEntries(this.settings.mapper(id).params) : {};
    const query = this.buildQueryParams(mappedParams, opts?.extraParams ?? {});
    const res$ = this.client.get<PagedList<TDto>>(this.buildUrl(suffix, query));
    return await lastValueFrom(res$);
  }

  public async post(id: TId, body: TCreateDto, opts?: { useSuffix?: boolean, extraParams?: Record<string, any> }): Promise<TDto> {
    let suffix = '';
    if (opts?.useSuffix ?? this.settings.postSuffix) {
      suffix += `/${this.settings.postSuffix}`;
    }

    const queryParts: Record<string, any>[] = [];

    if (this.settings.queryParam) {
      queryParts.push({ [this.settings.queryParam]: id });
    }

    if (id != null && id != '') {
      queryParts.push(Object.fromEntries(this.settings.mapper(id).params));
    }

    if (opts?.extraParams) {
      queryParts.push(opts.extraParams);
    }

    const query = this.buildQueryParams(...queryParts);
    const res$ = this.client.post<TDto>(this.buildUrl(suffix, query), body);
    return await lastValueFrom(res$);
  }

  public async put(id: TId, body: TEditDto, opts?: { useSuffix?: boolean, extraParams?: Record<string, any> }): Promise<TDto> {
    let suffix = '';
    if (opts?.useSuffix ?? this.settings.putSuffix) {
      suffix = `/${this.settings.putSuffix}`;
    }

    const mappedParams = Object.fromEntries(this.settings.mapper(id).params);
    const query = this.buildQueryParams(mappedParams, opts?.extraParams ?? {});
    const res$ = this.client.put<TDto>(this.buildUrl(suffix, query), body);
    return await lastValueFrom(res$);
  }

  public async delete(id: TId, opts?: { extraParams?: Record<string, any> }): Promise<void> {
    const mappedParams = Object.fromEntries(this.settings.mapper(id).params);
    const query = this.buildQueryParams(mappedParams, opts?.extraParams ?? {});
    await lastValueFrom(this.client.delete(this.buildUrl('', query)));
  }

  public async getPagedRequestAsync(params: PagedRequest, id?: TId): Promise<PagedList<TDto>> {
    const queryParams: Record<string, any> = {
      SkipCount: params.skipCount,
      MaxResultCount: params.maxResultCount
    };

    Object.entries(params).forEach(([key, value]) => {
      if (!['skipCount', 'maxResultCount'].includes(key) && value != null) {
        queryParams[key] = value;
      }
    });

    let suffix = '';
    if (id != null) suffix += `/${id}`;
    if (this.settings.getAllSuffix) suffix += `/${this.settings.getAllSuffix}`;

    const query = this.buildQueryParams(queryParams);
    const url = this.buildUrl(suffix, query);
    const res$ = this.client.get<PagedList<TDto>>(url);
    return await lastValueFrom(res$);
  }
}
