import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {BehaviorSubject, of} from 'rxjs';
import {NovaFakturaDataService} from '../../../ucetnictvi/nova-faktura/data/nova-faktura-data.service';
import {DialogService} from '../../../shared/dialog/dialog.service';
import {BreadcrumbService} from '../../../shared/breadcrumb/breadcrumb.service';
import {NovaFakturaComponent} from '../../../ucetnictvi/nova-faktura/nova-faktura.component';

import {HttpClientTestingModule} from '@angular/common/http/testing';
import {SubjektyDataService} from '../../../ucetnictvi/subjekty/data/subjekty-data.service';
import {SkladyPolozkyDataService} from '../../../sklady/sklady-polozky/data/sklady-polozky-data.service';
import {PolozkyFakturyDataService} from '../../../ucetnictvi/polozky-faktury/data/polozky-faktury-data.service';
import {DrawerService} from '../../../shared/layout/drawer.service';
import {FormControl, FormGroup} from '@angular/forms';

describe('NovaFakturaComponent - edit mode', () => {
  let component: NovaFakturaComponent;
  let fixture: ComponentFixture<NovaFakturaComponent>;

  const mockDialogService = jasmine.createSpyObj('DialogService', ['form', 'alert']);
  const mockBreadcrumbService = { breadcrumbsValue: [] };
  const mockNovaFakturaDataService = jasmine.createSpyObj('NovaFakturaDataService', ['get', 'post', 'put']);
  const mockSubjektyDataService = jasmine.createSpyObj('SubjektyDataService', ['getAll','get', 'post', 'put']);
  const mockSkladyService = jasmine.createSpyObj('SkladyPolozkyDataService', ['getAll', 'get', 'post', 'put']);
  const mockPolozkyFakturyService = jasmine.createSpyObj('PolozkyFakturyDataService', ['get', 'post', 'put', 'getAll']);

  const drawerOpenSubject = new BehaviorSubject<boolean>(false);
  const mockDrawerService = {
    openWithTemplate: jasmine.createSpy('openWithTemplate').and.callFake(() => {drawerOpenSubject.next(true);}),
    close: jasmine.createSpy('close').and.callFake(() => {drawerOpenSubject.next(false);}),
    isOpen: () => drawerOpenSubject.value,
    drawerOpen$: drawerOpenSubject.asObservable()
  };

  beforeEach(async () => {
    mockNovaFakturaDataService.get.and.resolveTo({
      id: '123',
      invoiceNumber: 'F123',
      supplier: { id: '1', name: '', ic: '', dic: '' },
      customer: { id: '2', name: '', ic: '', dic: '' },
      issueDate: '2024-01-01',
      totalNetAmount: 100,
      totalVatAmount: 21,
      totalGrossAmount: 121,
      items: []
    });

    mockSubjektyDataService.getAll.and.resolveTo({
      items: [
        { id: '1', name: 'Dodavatel', ic: '123', dic: 'CZ123' },
        { id: '2', name: 'Odběratel', ic: '456', dic: 'CZ456' }
      ]
    });

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, NovaFakturaComponent],
      providers: [
        { provide: DialogService, useValue: mockDialogService },
        { provide: NovaFakturaDataService, useValue: mockNovaFakturaDataService },
        { provide: BreadcrumbService, useValue: mockBreadcrumbService },
        { provide: SubjektyDataService, useValue: mockSubjektyDataService },
        { provide: SkladyPolozkyDataService, useValue: mockSkladyService },
        { provide: PolozkyFakturyDataService, useValue: mockPolozkyFakturyService },
        { provide: DrawerService, useValue: mockDrawerService },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of({ get: (key: string) => '123' }),
            snapshot: {
              paramMap: { get: (key: string) => '123' },
              data: {
                basePath: 'ucetnictvi',
                breadcrumb: 'Editace faktury'
              }
            }
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(NovaFakturaComponent);
    component = fixture.componentInstance;
  });


  afterEach(() => {
    mockNovaFakturaDataService.put.calls.reset();
    mockPolozkyFakturyService.post.calls.reset();
    mockDialogService.alert.calls.reset();
  }); //potrebuji cista data po testech an edit apod.

  it('should open and close drawer', async () => {
    await component.ngOnInit();
    component.drawerTemplate = {} as any;

    component.toggleDrawerWithContent();
    expect(mockDrawerService.openWithTemplate).toHaveBeenCalled();
    expect(component.drawerOpen).toBeTrue();

    component.toggleDrawerWithContent();
    expect(mockDrawerService.close).toHaveBeenCalled();
    expect(component.drawerOpen).toBeFalse();
  });

  it('should reset form to default values', async () => {
    await component.ngOnInit();

    component.baseFormDefaults = {
      invoiceNumber: 'F123',
      supplierId: '1',
      customerId: '2',
      issueDate: '2024-01-01',
      totalNetAmount: 100,
      totalVatAmount: 21,
      totalGrossAmount: 121
    };

    component.resetFormToDefaults();

    expect(component.invoiceItems.length).toBe(0);
    expect(component.formReady).toBeFalse();
    setTimeout(() => expect(component.formReady).toBeTrue(), 0);
  });

  it('should show error dialog if saving invoice fails', async () => {
    component.entityId = '123';
    component.isNew = false;

    component.baseForm = new FormGroup({
      invoiceNumber: new FormControl('F123'),
      supplierId: new FormControl({ value: '1', disabled: false }),
      customerId: new FormControl({ value: '2', disabled: false }),
      issueDate: new FormControl('2024-01-01'),
      taxDate: new FormControl(null),
      deliveryDate: new FormControl(null),
      totalNetAmount: new FormControl(100),
      totalVatAmount: new FormControl(21),
      totalGrossAmount: new FormControl(121),
      paymentTerms: new FormControl(null),
      vatRate: new FormControl(88),
      variableSymbol: new FormControl(null),
      constantSymbol: new FormControl(null),
      specificSymbol: new FormControl(null)
    });

    mockNovaFakturaDataService.put.and.rejectWith({ error: { error: { message: 'Chyba při ukládání' }}});

    await component.saveFaktura();

    expect(mockDialogService.alert).toHaveBeenCalledWith(jasmine.objectContaining({
      title: 'Chyba',
      message: 'Chyba při ukládání',
      dialogType: jasmine.anything()
    }));
  });

  it('should save edited invoice and call items save', async () => {
    component.entityId = '123';
    component.isNew = false;

    component.baseForm = new FormGroup({
      invoiceNumber: new FormControl('F123'),
      supplierId: new FormControl({ value: '1', disabled: false }),
      customerId: new FormControl({ value: '2', disabled: false }),
      issueDate: new FormControl('2024-01-01'),
      taxDate: new FormControl(null),
      deliveryDate: new FormControl(null),
      totalNetAmount: new FormControl(100),
      totalVatAmount: new FormControl(21),
      totalGrossAmount: new FormControl(121),
      paymentTerms: new FormControl(null),
      vatRate: new FormControl({ value: 88, disabled: false }),
      variableSymbol: new FormControl(null),
      constantSymbol: new FormControl(null),
      specificSymbol: new FormControl(null)
    });

    component.invoiceItems = [{
      id: 'item1',
      description: 'Test',
      quantity: 1,
      unitPrice: 100,
      vatRate: 88,
      netAmount: 100,
      vatAmount: 0,
      grossAmount: 100
    }];
    component.invoiceItemsForDelete = [];
    component.formReady = true;

    // vynucená validita
    spyOnProperty(component.baseForm, 'valid', 'get').and.returnValue(true);

    await component.saveFaktura();

    expect(mockNovaFakturaDataService.put).toHaveBeenCalledWith('123', jasmine.any(Object));
    expect(mockPolozkyFakturyService.post).toHaveBeenCalledWith(
      '',
      jasmine.objectContaining({
        invoiceId: '123',
        items: jasmine.any(Array),
        itemsForDelete: []
      }),
      { useSuffix: true }
    );
  });
});
