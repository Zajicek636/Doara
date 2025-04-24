import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {debounceTime, distinctUntilChanged, Subject} from 'rxjs';
import {ColumnSetting, TableSettings} from './table.settings';
import {CacheService} from '../../cache/cache.service';
import {DialogService} from '../../dialog/dialog.service';
import {DialogType} from '../../dialog/dialog.interfaces';
import {fieldsToColumns} from '../../forms/form-field.utils';

@Component({
  selector: 'app-dynamic-table',
  templateUrl: './table.component.html',
  standalone: false,
  styleUrls: ['./table.component.scss']
})
export class DynamicTableComponent<T> implements OnInit, AfterViewInit {
  @Input() settings!: TableSettings;
  @Input() dataService!: { getPagedRequest: (params: any) => Promise<T[]> };
  @Input() data?: T[];

  @Output() rowDoubleClicked = new EventEmitter<T>();
  @Output() selectedElement = new EventEmitter<T>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<T>([]);
  displayedColumns: ColumnSetting<T>[] = [];
  columnKeys: string[] = [];
  expandedElement?: T;

  private filterSubject = new Subject<string>();
  currentFilter = '';

  constructor(
    private cacheService: CacheService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    const visibleFields = this.settings.formFields.filter(f => f.showInTable === true);
    this.displayedColumns = fieldsToColumns<T>(visibleFields);this.columnKeys = this.displayedColumns.map(c => c.key);

    // Filter predicate uses only valueGetter
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
      this.dataSource.filter = filterValue;
      if (this.paginator) this.paginator.pageIndex = 0;
    });

    this.loadData();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.paginator.page.subscribe(() => this.loadData());
    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0;
      this.loadData();
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterSubject.next(filterValue);
  }

  private buildQueryParams(): any {
    const params = new URLSearchParams();
    if (this.currentFilter) params.set('filter', this.currentFilter);
    if (this.settings.extraQueryParams) {
      Object.entries(this.settings.extraQueryParams)
        .filter(([_, v]) => v != null)
        .forEach(([k, v]) => params.set(k, v));
    }
    if (this.paginator) {
      params.set('page', this.paginator.pageIndex.toString());
      params.set('pageSize', (this.paginator.pageSize || this.settings.defaultPageSize || 10).toString());
    } else {
      params.set('page', '0');
      params.set('pageSize', (this.settings.defaultPageSize || 10).toString());
    }
    if (this.sort && this.sort.active) {
      params.set('sortField', this.sort.active);
      params.set('sortOrder', this.sort.direction);
    }
    const obj: Record<string, any> = {};
    params.forEach((v, k) => obj[k] = v);
    return obj;
  }

  async loadData(): Promise<void> {
    if (this.data) {
      this.dataSource.data = this.data;
      return;
    }
    try {
      const params = this.buildQueryParams();
      const result = await this.dataService.getPagedRequest(params);
      this.dataSource.data = result;
    } catch (error) {
      await this.dialogService.alert({
        title: 'Chyba',
        dialogType: DialogType.ERROR,
        message: 'Chyba při načítání dat z externího zdroje'
      });
      this.dataSource.data = [];
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
