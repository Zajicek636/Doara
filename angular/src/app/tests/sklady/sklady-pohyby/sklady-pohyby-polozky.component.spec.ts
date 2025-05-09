import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';
import {SkladyPohybyPolozkyComponent} from '../../../sklady/sklady-editace/sklady-pohyby-polozky.component';
import {DialogService} from '../../../shared/dialog/dialog.service';
import {SkladyPohybyPolozkyDataService} from '../../../sklady/sklady-editace/data/sklady-pohyby-polozky-data.service';
import {PolozkaKontejneruDataService} from '../../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {MovementCategory, MovementDto} from '../../../sklady/sklady-editace/data/sklady-pohyby-polozky.interfaces';
import {BreadcrumbService} from '../../../shared/breadcrumb/breadcrumb.service';

describe('SkladyPohybyPolozkyComponent', () => {
  let component: SkladyPohybyPolozkyComponent;
  let fixture: ComponentFixture<SkladyPohybyPolozkyComponent>;

  let mockDialogService: jasmine.SpyObj<DialogService>;
  let mockDataService: jasmine.SpyObj<SkladyPohybyPolozkyDataService>;
  let mockPolozkaService: jasmine.SpyObj<PolozkaKontejneruDataService>;

  const dummyMovements: MovementDto[] = [
    { id: 'm1', quantity: 5, movementCategory: MovementCategory.Unused },
    { id: 'm2', quantity: 3, movementCategory: MovementCategory.Used }
  ];

  beforeEach(async () => {
    mockDialogService = jasmine.createSpyObj('DialogService', ['form', 'alert', 'confirmAsync']);
    mockDataService = jasmine.createSpyObj('SkladyPohybyPolozkyDataService', ['postBySuffix']);
    mockPolozkaService = jasmine.createSpyObj('PolozkaKontejneruDataService', ['get']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, SkladyPohybyPolozkyComponent],
      providers: [
        { provide: SkladyPohybyPolozkyDataService, useValue: mockDataService },
        { provide: PolozkaKontejneruDataService, useValue: mockPolozkaService },
        { provide: DialogService, useValue: mockDialogService },
        { provide: BreadcrumbService, useValue: { breadcrumbsValue: [] } },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of({ get: (key: string) => '1' }),
            snapshot: {
              paramMap: {
                get: (key: string) => '1'
              },
              data: {
                basePath: 'sklady',
                breadcrumb: 'Pohyby'
              }
            }
          }
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(SkladyPohybyPolozkyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create and load data', async () => {
    mockPolozkaService.get.and.resolveTo({
      containerId: 'c1',
      movements: dummyMovements
    });
    await component.loadData();
    expect(component.dataSource.length).toBe(2);
    expect(component.loaded).toBeTrue();
  });

  it('should not proceed in onAdd if dialog cancelled', async () => {
    mockDialogService.form.and.resolveTo(undefined);
    await component.onAdd();
    expect(mockDialogService.form).toHaveBeenCalled();
    expect(mockDataService.postBySuffix).not.toHaveBeenCalled();
  });

  it('should call postBySuffix and update data on successful add', async () => {
    mockDialogService.form.and.resolveTo({
      main_section: { data: { quantity: 99 }, valid: true, modified: true, form: {} as any }
    });
    mockDataService.postBySuffix.and.resolveTo({ movements: dummyMovements });

    component.tableComponent = { dataSource: { data: [] } } as any;

    await component.onAdd();

    expect(mockDataService.postBySuffix).toHaveBeenCalled();
    expect(component.tableComponent.dataSource.data.length).toBe(2);
  });

  it('should handle error on postBySuffix in onAdd', async () => {
    mockDialogService.form.and.resolveTo({
      main_section: { data: { quantity: 99 }, valid: true, modified: true, form: {} as any }
    });

    mockDataService.postBySuffix.and.rejectWith({ error: { error: { message: 'Add failed' } } });

    component.tableComponent = { dataSource: { data: [] } } as any;

    await component.onAdd();
    expect(mockDialogService.alert).toHaveBeenCalledWith(jasmine.objectContaining({ message: 'Add failed' }));
  });

  it('should not proceed with delete when user cancels confirmation', async () => {
    mockDialogService.confirmAsync.and.resolveTo(false);
    await component.onDelete();
    expect(mockDataService.postBySuffix).not.toHaveBeenCalled();
  });

  it('should delete movement and update table data', async () => {
    const elementToDelete: MovementDto = { id: 'm1', quantity: 10, movementCategory: MovementCategory.Unused };
    component.chosenElement = elementToDelete;
    component.tableComponent = {
      dataSource: { data: [elementToDelete] }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(true);
    mockDataService.postBySuffix.and.resolveTo();

    await component.onDelete();

    expect(mockDataService.postBySuffix).toHaveBeenCalled();
    expect(component.tableComponent.dataSource.data.length).toBe(0);
    expect(component.chosenElement).toBeUndefined();
  });

  it('should show error when delete fails', async () => {
    const failingElement: MovementDto = { id: 'fail', quantity: 1, movementCategory: MovementCategory.Unused };
    component.chosenElement = failingElement;
    component.tableComponent = {
      dataSource: { data: [failingElement] }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(true);
    mockDataService.postBySuffix.and.rejectWith({ error: { error: { message: 'Delete error' } } });

    await component.onDelete();

    expect(mockDialogService.alert).toHaveBeenCalledWith(jasmine.objectContaining({ message: 'Delete error' }));
  });

  it('should skip deletion logic if chosenElement is null', async () => {
    component.chosenElement = undefined!;
    await component.onDelete();
    expect(mockDialogService.confirmAsync).not.toHaveBeenCalled();
  });
});
