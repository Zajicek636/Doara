import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import {SeznamFakturComponent} from '../../../ucetnictvi/seznam-faktur/seznam-faktur.component';
import {SeznamFakurDataService} from '../../../ucetnictvi/seznam-faktur/data/seznam-fakur-data.service';
import {DialogService} from '../../../shared/dialog/dialog.service';
import {BreadcrumbService} from '../../../shared/breadcrumb/breadcrumb.service';
describe('SeznamFakturComponent', () => {
  let component: SeznamFakturComponent;
  let fixture: ComponentFixture<SeznamFakturComponent>;

  const mockDataService = jasmine.createSpyObj('SeznamFakurDataService', ['delete']);
  const mockDialogService = jasmine.createSpyObj('DialogService', ['confirmAsync']);
  const mockBreadcrumbService = { breadcrumbsValue: [{ label: 'Seznam', url: '/seznam' }] };
  const mockRouter = jasmine.createSpyObj('Router', ['navigate']);
  const mockActivatedRoute = {
    snapshot: {
      data: {},
      paramMap: { get: () => null }
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [],
      imports: [SeznamFakturComponent, RouterTestingModule, HttpClientTestingModule],
      providers: [
        { provide: SeznamFakurDataService, useValue: mockDataService },
        { provide: DialogService, useValue: mockDialogService },
        { provide: BreadcrumbService, useValue: mockBreadcrumbService },
        { provide: Router, useValue: mockRouter },
        { provide: ActivatedRoute, useValue: mockActivatedRoute }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SeznamFakturComponent);
    component = fixture.componentInstance;
  });

  afterEach(() => {
    mockDialogService.confirmAsync.calls.reset();
    mockDataService.delete.calls.reset();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to new invoice on onAdd()', () => {
    component.basePath = '';
    component.onAdd();
    expect(mockRouter.navigate).toHaveBeenCalledWith(
      ['', 'faktura'],
      { state: { previousBreadcrumbs: mockBreadcrumbService.breadcrumbsValue } }
    );
  });

  it('should navigate to edit invoice on onEdit()', () => {
    component.chosenElement = { id: '123' } as any;
    component.basePath = '';
    component.onEdit();
    expect(mockRouter.navigate).toHaveBeenCalledWith(
      ['', 'faktura', '123'],
      { state: { previousBreadcrumbs: mockBreadcrumbService.breadcrumbsValue } }
    );
  });

  it('should delete invoice after confirmation', async () => {
    const fakeItem = { id: 'abc' };
    component.chosenElement = fakeItem as any;
    component.tableComponent = {
      dataSource: {
        data: [fakeItem]
      }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(true);
    mockDataService.delete.and.resolveTo(null);

    await component.onDelete();

    expect(mockDataService.delete).toHaveBeenCalledWith('abc');
    expect(component.tableComponent.dataSource.data).toEqual([]);
    expect(component.chosenElement).toBeUndefined();
  });

  it('should not delete if confirmation is cancelled', async () => {
    component.chosenElement = { id: 'abc' } as any;
    component.tableComponent = {
      dataSource: { data: [component.chosenElement] }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(false);
    await component.onDelete();

    expect(mockDataService.delete).not.toHaveBeenCalled();
    expect(component.tableComponent.dataSource.data.length).toBe(1);
  });

  it('should call onEdit on handleDoubleClick()', () => {
    const spy = spyOn(component, 'onEdit');
    component.handleDoubleClick({});
    expect(spy).toHaveBeenCalled();
  });

  it('should set chosenElement on clickedElement()', () => {
    const row = { id: 'test' } as any;
    component.clickedElement(row);
    expect(component.chosenElement).toBe(row);
  });
});
