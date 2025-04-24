import {FormField} from '../../forms/form.interfaces';

export interface TableSettings {
  formFields: FormField[];
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
