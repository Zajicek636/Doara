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
export class DynamicTableComponent implements OnInit, AfterViewInit {
  @Input() settings!: TableSettings;
  @Input() dataService!: { getPagedRequest: (params: any) => Promise<any[]> };

  // Event emitter pro dvojklik na řádek
  @Output() rowDoubleClicked: EventEmitter<any> = new EventEmitter<any>();

  dataSource = new MatTableDataSource<any>([]);
  displayedColumns: string[] = [];
  expandedElement: any = undefined;


  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  // Subject pro debounce filtru
  private filterSubject = new Subject<string>();
  currentFilter: string = '';

  constructor(
    private cacheService: CacheService,
    private dialogService: DialogService
  )
  {

  }

  ngOnInit(): void {
    // Pokud máte definované sloupce jako stringové pole
    this.displayedColumns = this.settings.displayedColumns;

    // Nastavení vestavěného filter predicate, pokud chcete filtrovat dle všech polí
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

    // Načtení prvních dat – pro účely demonstrace s mock daty
    this.loadData();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    // Na změnu stránky načteme nová data (server-side) nebo necháme paginator pracovat s klientskými daty
    this.paginator.page.subscribe((pageEvent: PageEvent) => {
      this.loadData();
    });

    // Na změnu řazení resetujeme stránkovací index a načteme data
    this.sort.sortChange.subscribe((sort: Sort) => {
      this.paginator.pageIndex = 0;
      this.loadData();
    });
  }

  /**
   * Metoda volaná při zadání filtru uživatelem.
   * Používáme vestavěné filtrování MatTableDataSource.
   */
  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterSubject.next(filterValue);
  }

  /**
   * Sestaví query parametry pro volání metody getPagedRequest.
   */
  private buildQueryParams(): any {
    let params = new URLSearchParams();

    // Přidání filtru, pokud je zadán
    if (this.currentFilter) {
      params.set('filter', this.currentFilter);
    }

    // Přidání dodatečných parametrů z nastavení
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

    // Řazení
    if (this.sort && this.sort.active) {
      params.set('sortField', this.sort.active);
      params.set('sortOrder', this.sort.direction);
    }
    // Převod na objekt – HttpClient totiž očekává prostý objekt s klíči a hodnotami
    const paramsObj: any = {};
    params.forEach((value, key) => {
      paramsObj[key] = value;
    });
    return paramsObj;
  }

  /**
   * Načte data voláním metody getPagedRequest předané datové služby s aktuálními parametry.
   * Pro účely demonstrace zde využíváme namockovaná data.
   */
  async loadData(): Promise<void> {
    const params = this.buildQueryParams();

/*
    this.dataService.getPagedRequest(params)
      .then((data: any[]) => {
        this.dataSource.data = data;
      })
      .catch(error => {
        this.dialogService.alert({
          title: "Chyba",
          dialogType: DialogType.ERROR,
          message: "Chyba při načítání dat z externího zdroje"
        })
        this.dataSource.data = [];
      });
*/

    const b: any[] = [];
    for (let a = 0; a < 999; a++) {
      b.push({ id: `ANO+${a}`, name: `Jméno ${a}`, progress: Math.round(Math.random() * 100), fruit: ['jablko', 'hruška', 'banán'][a % 3] });
    }
    this.dataSource.data = b;
  }

  onRowDoubleClick(row: any): void {
    this.rowDoubleClicked.emit(row);
  }

  toggleRow(row: any) {
    this.expandedElement = row;
  }
}
