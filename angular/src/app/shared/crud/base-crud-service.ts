import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

export interface QueryParams {
  params: Map<string, any>;
}

export interface CrudSettings<TId, TEntity> {
  baseUrl: string;
  postUrl?: string;
  queryParam?: string;
  mapper: (id: TId) => QueryParams;
  idGetter: (entity: TEntity) => TId;
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

  public async get(id: TId): Promise<TDto> {
    const qString = this.toQueryString(this.settings.mapper(id));
    const res$ = this.client.get<TDto>(`${this.settings.baseUrl}/${id}${qString}`);
    return await lastValueFrom(res$);
  }

  public async post<T>(id: TId, body: TDto): Promise<T> {
    const url = this.settings.postUrl ?? this.settings.baseUrl;
    const res$ = this.client.post<T>(`${url}${this.settings.queryParam ? '?' + this.settings.queryParam : ''}${id ? '=' + id : ''}`, body);
    return await lastValueFrom(res$);
  }

  public async put(id: TId, body: TEditDto): Promise<TDto> {
    const qString = this.toQueryString(this.settings.mapper(id));
    const res$ = this.client.put<TDto>(`${this.settings.baseUrl}/${id}${qString}`, body);
    return await lastValueFrom(res$);
  }

  public async delete(id: TId): Promise<void> {
    const qString = this.toQueryString(this.settings.mapper(id));
    await lastValueFrom(this.client.delete(`${this.settings.baseUrl}/${id}${qString}`));
  }
}
