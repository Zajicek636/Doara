import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { By } from '@angular/platform-browser';
import { of } from 'rxjs';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {DialogService} from '../../shared/dialog/dialog.service';

describe('DynamicTableComponent', () => {
  let component: DynamicTableComponent<any>;
  let fixture: ComponentFixture<DynamicTableComponent<any>>;

  const mockDialogService = jasmine.createSpyObj('DialogService', ['alert']);

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DynamicTableComponent],
      imports: [MatPaginatorModule, MatSortModule, MatTableModule, NoopAnimationsModule],
      providers: [{ provide: DialogService, useValue: mockDialogService }]
    }).compileComponents();

    fixture = TestBed.createComponent(DynamicTableComponent<any>);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set columns from settings', () => {
    component.settings = {
      cacheEntityType: '',
      clickable: false,
      expandable: false,
      columns: [
        { key: 'name', header: 'Jméno', valueGetter: (r: any) => r.name },
        { key: 'age', header: 'Věk', valueGetter: (r: any) => r.age }
      ],
      defaultPageSize: 10,
      showPaginator: true,
      pageSizeOptions: [10, 20]
    };

    component.data = [
      { name: 'Alice', age: 25 },
      { name: 'Bob', age: 30 }
    ];

    fixture.detectChanges();
    component.ngOnInit();

    expect(component.columnKeys).toEqual(['name', 'age']);
    expect(component.dataSource.data.length).toBe(2);
  });

  it('should call dataService when loading data', async () => {
    const mockService = {
      getPagedRequestAsync: jasmine.createSpy('getPagedRequestAsync').and.resolveTo({
        items: [{ name: 'test' }],
        totalCount: 1
      })
    };

    component.settings = {
      cacheEntityType: '',
      clickable: false,
      expandable: false,
      columns: [{ key: 'name', header: 'Jméno', valueGetter: (r: any) => r.name }],
      defaultPageSize: 10,
      showPaginator: true,
      pageSizeOptions: [10]
    };
    component.dataService = mockService;
    component.data = undefined;

    fixture.detectChanges();
    await component.loadData();

    expect(mockService.getPagedRequestAsync).toHaveBeenCalled();
    expect(component.dataSource.data.length).toBe(1);
  });

  it('should emit selected element on row click', () => {
    const testRow = { name: 'A' };
    spyOn(component.selectedElement, 'emit');
    component.settings = {
      columns: [{ key: 'name', header: 'Jméno', valueGetter: (r: any) => r.name }],
      clickable: true
    } as any;
    component.toggleRow(testRow);
    expect(component.expandedElement).toBe(testRow);
    expect(component.selectedElement.emit).toHaveBeenCalledWith(testRow);
  });

  it('should emit rowDoubleClicked on double click', () => {
    const testRow = { name: 'B' };
    spyOn(component.rowDoubleClicked, 'emit');
    component.settings = {
      columns: [],
      clickable: true
    } as any;

    component.onRowDoubleClick(testRow);
    expect(component.rowDoubleClicked.emit).toHaveBeenCalledWith(testRow);
  });
});
