import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';
import { DialogService } from '../../../shared/dialog/dialog.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {PolozkaKontejneruComponent} from '../../../sklady/polozka-kontejneru/polozka-kontejneru.component';
import {PolozkaKontejneruDataService} from '../../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {ContainerItemDto} from '../../../sklady/polozka-kontejneru/data/polozka-kontejneru.interfaces';
import {of} from 'rxjs';

describe('PolozkaKontejneruComponent', () => {
  let component: PolozkaKontejneruComponent;
  let fixture: ComponentFixture<PolozkaKontejneruComponent>;
  let mockDialogService: jasmine.SpyObj<DialogService>;
  let mockDataService: jasmine.SpyObj<PolozkaKontejneruDataService>;

  const dummyItem: ContainerItemDto = {
    id: '1',
    name: 'Test item',
    containerId: 'c1'
  };

  beforeEach(async () => {
    mockDialogService = jasmine.createSpyObj('DialogService', ['form', 'alert', 'confirmAsync']);
    mockDataService = jasmine.createSpyObj('PolozkaKontejneruDataService', ['post', 'put', 'delete']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule,PolozkaKontejneruComponent],
      providers: [
        { provide: PolozkaKontejneruDataService, useValue: mockDataService },
        { provide: DialogService, useValue: mockDialogService },
        { provide: BreadcrumbService, useValue: { breadcrumbsValue: [] } },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of({
              get: (key: string) => (key === 'id' ? 'c1' : null)
            }),
            snapshot: {
              paramMap: {
                get: (key: string) => (key === 'id' ? 'c1' : null)
              },
              data: {
                basePath: 'sklady',
                breadcrumb: 'Kontejner'
              }
            }
          }
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA] // kvůli <app-dynamic-table> apod.
    }).compileComponents();

    fixture = TestBed.createComponent(PolozkaKontejneruComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create and initialize entityId from route', () => {
    expect(component).toBeTruthy();
    expect(component.entityId).toBe('c1');
    expect(component.tableSettings?.extraQueryParams?.['ContainerId']).toBe('c1');
  });

  it('should handle addNewItemContainer correctly', async () => {
    mockDialogService.form.and.resolveTo({
      main_section: {
        valid: true,
        modified: true,
        form: {} as any,
        data: {
          name: 'new item',
          quantityType: { value: 1 },
          realPrice: 100,
          markup: 10
        }
      },
      second_section: {
        valid: true,
        modified: false,
        form: {} as any,
        data: {}
      }
    });

    mockDataService.post.and.resolveTo(dummyItem);

    component.tableComponent = {
      dataSource: { data: [] }
    } as any;

    await component.addNewItemContainer();
    expect(mockDialogService.form).toHaveBeenCalled();
    expect(mockDataService.post).toHaveBeenCalled();
    expect(component.tableComponent.dataSource.data.length).toBe(1);
  });

  it('should call deleteItemContainer with confirmation and remove item', async () => {
    component.chosenElement = dummyItem;
    component.tableComponent = {
      dataSource: { data: [dummyItem] }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(true);
    mockDataService.delete.and.resolveTo();

    await component.deleteItemContainer();

    expect(mockDialogService.confirmAsync).toHaveBeenCalled();
    expect(mockDataService.delete).toHaveBeenCalledWith('1');
    expect(component.tableComponent.dataSource.data.length).toBe(0);
  });

  it('should show error dialog if delete fails', async () => {
    component.chosenElement = dummyItem;
    component.tableComponent = { dataSource: { data: [dummyItem] } } as any;

    mockDialogService.confirmAsync.and.resolveTo(true);
    mockDataService.delete.and.rejectWith({ error: { error: { message: 'Delete failed' } } });

    await component.deleteItemContainer();
    expect(mockDialogService.alert).toHaveBeenCalledWith(jasmine.objectContaining({ message: 'Delete failed' }));
  });

  it('should edit item and update table data', async () => {
    component.chosenElement = dummyItem;
    component.tableComponent = {
      dataSource: { data: [dummyItem] }
    } as any;

    mockDialogService.form.and.resolveTo({
      main_section: {
        valid: true,
        modified: true,
        form: {} as any,
        data: {
          name: 'updated',
          quantityType: { value: 2 },
          realPrice: 200,
          markup: 20
        }
      },
      second_section: {
        valid: true,
        modified: false,
        form: {} as any,
        data: {}
      }
    });

    mockDataService.put.and.resolveTo({ ...dummyItem, name: 'updated' });

    await component.editItemContainer();

    expect(component.tableComponent.dataSource.data[0].name).toBe('updated');
  });

  it('should navigate to pohyby on double click', () => {
    const router = TestBed.inject(RouterTestingModule) as any;
    spyOn(component['router'], 'navigate');
    component.chosenElement = dummyItem;
    component.handleDoubleClick(dummyItem);
    expect(component['router'].navigate).toHaveBeenCalledWith(
      ['sklady', 'pohyby-polozky', '1'],
      { state: { previousBreadcrumbs: [] } }
    );
  });
});
