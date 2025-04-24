import {FormField} from '../../forms/form.interfaces';

export interface TableSettings<T> {
  formFields?: FormField[];
  columns?: ColumnSetting<T>[];
  pageSizeOptions?: number[];
  clickable: boolean;
  expandable: boolean;
  defaultPageSize?: number;
  cacheEntityType: string;
  extraQueryParams?: { [key: string]: any };
}

export interface ColumnSetting<T> {
  key: string;
  prop?: string;
  header?: string;
  valueGetter: (row: T) => string;
}
