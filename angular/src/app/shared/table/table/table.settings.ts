
export interface TableSettings {
  displayedColumns: string[];
  pageSizeOptions?: number[];
  clickable: boolean,
  expandable: boolean,
  defaultPageSize?: number;
  cacheEntityType: string,
  extraQueryParams?: { [key: string]: any };
}
