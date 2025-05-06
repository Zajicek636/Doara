import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {debounceTime, distinctUntilChanged, Subject} from 'rxjs';
import {ColumnSetting, TableSettings} from './table.settings';
import {CacheService} from '../../cache/cache.service';
import {DialogService} from '../../dialog/dialog.service';
import {DialogType} from '../../dialog/dialog.interfaces';
import {fieldsToColumns} from '../../forms/form-field.utils';
import {PagedList, PagedRequest} from "../../crud/base-crud-service";

@Component({
  selector: 'app-dynamic-table',
  templateUrl: './table.component.html',
  standalone: false,
  styleUrls: ['./table.component.scss']
})
export class DynamicTableComponent<T> implements OnInit, AfterViewInit {
  @Input() settings!: TableSettings<T>;
  @Input() dataService!: { getPagedRequestAsync: (params: PagedRequest) => Promise<PagedList<T>> };
  @Input() data?: T[];

  @Output() rowDoubleClicked = new EventEmitter<T>();
  @Output() selectedElement = new EventEmitter<T>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<T>([]);
  displayedColumns: ColumnSetting<T>[] = [];
  columnKeys: string[] = [];
  expandedElement?: T;
  totalCount = 0;

  private pageCache = new Map<number, T[]>();
  private isLoading = false;

  private filterSubject = new Subject<string>();
  currentFilter = '';

  constructor(
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    let columns: ColumnSetting<T>[] = [];
    if (this.settings.columns) {
      columns = this.settings.columns;
    } else if (this.settings.formFields) {
      const visibleFields = this.settings.formFields.filter(f => f.visible === true);
      columns = fieldsToColumns<T>(visibleFields);
    } else {
      console.warn('DynamicTableComponent: Nebyly poskytnuty žádné sloupce (ani columns, ani formFields).');
    }

    this.displayedColumns = columns;
    this.columnKeys = columns.map(c => c.key);

    this.dataSource.filterPredicate = (data, filter) => {
      const dataStr = this.displayedColumns
        .map(col => col.valueGetter(data))
        .filter(v => v != null)
        .join(' ')
        .toLowerCase();
      return dataStr.includes(filter);
    };

    this.filterSubject.pipe(
        debounceTime(300),
        distinctUntilChanged()
    ).subscribe(filterValue => {
      this.currentFilter = filterValue;
      this.pageCache.clear();
      this.paginator.pageIndex = 0;
      this.loadData();
    });

    this.loadData();
  }

  ngAfterViewInit(): void {
    this.paginator.page.subscribe(event => {
      if (event.pageSize !== this.settings.defaultPageSize) {
        this.pageCache.clear();
        this.settings.defaultPageSize = event.pageSize;
      }
      this.loadData();
    });
    this.paginator.pageSize = this.settings.defaultPageSize ?? 10;

    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0;
      this.pageCache.clear();
      this.loadData();
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterSubject.next(filterValue);
  }

  private buildQueryParams(): PagedRequest {
    const pageIndex = this.paginator?.pageIndex ?? 0;
    const pageSize = this.paginator?.pageSize ?? this.settings.defaultPageSize ?? 10;

    const skipCount = pageIndex * pageSize;
    const maxResultCount = pageSize;

    const params: PagedRequest = {
      skipCount,
      maxResultCount
    };

    if (this.currentFilter) {
      params['filter'] = this.currentFilter;
    }

    if (this.sort?.active) {
      params['sortField'] = this.sort.active;
      params['sortOrder'] = this.sort.direction;
    }

    if (this.settings.extraQueryParams) {
      Object.entries(this.settings.extraQueryParams)
          .filter(([_, v]) => v != null)
          .forEach(([k, v]) => params[k] = v);
    }

    return params;
  }

  async loadData(): Promise<void> {
    const pageIndex = this.paginator?.pageIndex ?? 0;
    const pageSize = this.paginator?.pageSize ?? this.settings.defaultPageSize ?? 10;

    // ✅ Pokud máš pevně daná data přes @Input
    if (this.data) {
      this.dataSource.data = this.data.slice(pageIndex * pageSize, (pageIndex + 1) * pageSize);
      this.totalCount = this.data.length;
      return;
    }

    // ✅ Pokud už je stránka v cache – použij ji
    if (this.pageCache.has(pageIndex)) {
      this.dataSource.data = this.pageCache.get(pageIndex)!;
      return;
    }

    try {
      this.isLoading = true;
      const params = this.buildQueryParams();
      const result = await this.dataService.getPagedRequestAsync(params);

      this.pageCache.set(pageIndex, result.items);
      this.dataSource.data = result.items;
      this.totalCount = result.totalCount;

      this.isLoading = false;
    } catch (error: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: error,
        dialogType: DialogType.ERROR
      })
      this.isLoading = false;
      this.dataSource.data = [];
      this.totalCount = 0;

      // Nepovinně: zobrazit dialog s chybou
      /*
      await this.dialogService.alert({
        title: 'Chyba',
        dialogType: DialogType.ERROR,
        message: 'Chyba při načítání dat z externího zdroje'
      });
      */
    }
  }

  onRowDoubleClick(row: T): void {
    this.rowDoubleClicked.emit(row);
  }

  toggleRow(row: T): void {
    this.expandedElement = row;
    this.selectedElement.emit(row);
  }
}
