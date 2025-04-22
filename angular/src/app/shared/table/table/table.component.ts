import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatSort, Sort} from '@angular/material/sort';
import {debounceTime, distinctUntilChanged, Subject} from 'rxjs';
import {TableSettings} from './table.settings';
import {CacheService} from '../../cache/cache.service';
import {DialogService} from '../../dialog/dialog.service';
import {DialogType} from '../../dialog/dialog.interfaces';

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

  @Output() rowDoubleClicked: EventEmitter<T> = new EventEmitter<T>();
  @Output() selectedElement: EventEmitter<T> = new EventEmitter<T>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<T>([]);
  displayedColumns: string[] = [];
  expandedElement: T | undefined;
  private filterSubject = new Subject<string>();
  currentFilter: string = '';

  constructor(
    private cacheService: CacheService,
    private dialogService: DialogService
  )
  {}

  ngOnInit(): void {
    this.displayedColumns = this.settings.displayedColumns;
    this.dataSource.filterPredicate = (data: any, filter: string) => {
      const accumulator = (currentTerm: string, key: string) => {
        return currentTerm + (data[key] ? data[key].toString().toLowerCase() : '');
      };
      const dataStr = this.displayedColumns.reduce(accumulator, '');
      return dataStr.indexOf(filter) !== -1;
    };

    this.filterSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(filterValue => {
      this.currentFilter = filterValue;
      this.dataSource.filter = filterValue;

      if (this.paginator) {
        this.paginator.pageIndex = 0;
      }
    });
    this.loadData();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.paginator.page.subscribe((pageEvent: PageEvent) => {
      this.loadData();
    });

    this.sort.sortChange.subscribe((sort: Sort) => {
      this.paginator.pageIndex = 0;
      this.loadData();
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterSubject.next(filterValue);
  }

  private buildQueryParams(): any {
    let params = new URLSearchParams();

    if (this.currentFilter) {
      params.set('filter', this.currentFilter);
    }
    if (this.settings.extraQueryParams) {
      Object.keys(this.settings.extraQueryParams).forEach(key => {
        if (this.settings.extraQueryParams![key] != null) {
          params.set(key, this.settings.extraQueryParams![key]);
        }
      });
    }

    // Paginace
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
    const paramsObj: any = {};
    params.forEach((value, key) => {
      paramsObj[key] = value;
    });
    return paramsObj;
  }

  async loadData(): Promise<void> {
    if (this.data) {
      this.dataSource.data = this.data;
      return;
    }

    if (this.dataService && this.dataService.getPagedRequest) {
      const params = this.buildQueryParams();
      try {
        const data = await this.dataService.getPagedRequest(params);
        this.dataSource.data = data;
      } catch (error) {
        await this.dialogService.alert({
          title: 'Chyba',
          dialogType: DialogType.ERROR,
          message: 'Chyba při načítání dat z externího zdroje'
        });
        this.dataSource.data = [];
      }
    }
  }

  onRowDoubleClick(row: T): void {
    this.rowDoubleClicked.emit(row);
  }

  toggleRow(row: T) {
    this.expandedElement = row;
    this.selectedElement.emit(row)
  }
}
