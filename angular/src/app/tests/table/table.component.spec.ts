import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { Component } from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {DynamicTableComponent} from '../../shared/table/table/table.component';
import {DialogService} from '../../shared/dialog/dialog.service';

interface TestItem {
  id: number;
  name: string;
}

@Component({
  standalone: true,
  selector: 'test-host',
  template: `
    <app-dynamic-table
      [settings]="settings"
      [dataService]="mockService"
      [data]="inputData"
      (rowDoubleClicked)="onDoubleClick($event)"
      (selectedElement)="onSelect($event)">
    </app-dynamic-table>`,
  imports: [
    SharedModule
  ]
})
class TestHostComponent {
  settings = {
    columns: [
      {
        key: 'name',
        header: 'Name',
        valueGetter: (row: TestItem) => row.name,
      },
    ],
    clickable: true,
    expandable: false,
    defaultPageSize: 5,
    cacheEntityType: 'TestEntity',
    showPaginator: true,
  };

  inputData: TestItem[] | undefined = undefined;
  mockService = {
    getPagedRequestAsync: jasmine.createSpy().and.resolveTo({
      items: Array.from({ length: 5 }, (_, i) => ({ id: i, name: `Item ${i}` })),
      totalCount: 5
    })
  };

  onDoubleClick = jasmine.createSpy();
  onSelect = jasmine.createSpy();
}

describe('DynamicTableComponent', () => {
  let fixture: ComponentFixture<TestHostComponent>;
  let host: TestHostComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DynamicTableComponent],
      imports: [
        TestHostComponent,
        SharedModule
      ],
      providers: [
        {
          provide: DialogService,
          useValue: {
            alert: jasmine.createSpy().and.returnValue(Promise.resolve())
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TestHostComponent);
    host = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the host and table component', () => {
    expect(host).toBeTruthy();
    const tableEl = fixture.debugElement.query(By.css('table'));
    expect(tableEl).toBeTruthy();
  });


  it('should handle error from dataService', async () => {
    const tableComponent = fixture.debugElement.query(By.directive(DynamicTableComponent)).componentInstance as DynamicTableComponent<TestItem>;
    const dialogService = TestBed.inject(DialogService);
    host.mockService.getPagedRequestAsync.and.rejectWith({ error: { error: { message: 'Test error' } } });
    tableComponent['pageCache'].clear();
    await tableComponent.loadData();
    expect(dialogService.alert).toHaveBeenCalledWith(jasmine.objectContaining({ message: 'Test error' }));
  });

  it('should not crash when paginator not defined', async () => {
    const component = fixture.debugElement.query(By.directive(DynamicTableComponent)).componentInstance as DynamicTableComponent<TestItem>;
    component.paginator = undefined!;
    const params = component['buildQueryParams']();
    expect(params.skipCount).toBe(0);
  });

  it('should cache pages after load', async () => {
    const component = fixture.debugElement.query(By.directive(DynamicTableComponent)).componentInstance as DynamicTableComponent<TestItem>;
    await component.loadData();
    expect(component['pageCache'].has(0)).toBeTrue();
    expect(component['pageCache'].get(0)?.length).toBe(5);
  });
});
