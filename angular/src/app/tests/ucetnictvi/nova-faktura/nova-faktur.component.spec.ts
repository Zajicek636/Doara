import {ComponentFixture, TestBed} from '@angular/core/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {NovaFakturaComponent} from '../../../ucetnictvi/nova-faktura/nova-faktura.component';
import {DialogService} from '../../../shared/dialog/dialog.service';
import {NovaFakturaDataService} from '../../../ucetnictvi/nova-faktura/data/nova-faktura-data.service';
import {BreadcrumbService} from '../../../shared/breadcrumb/breadcrumb.service';
import {SubjektyDataService} from '../../../ucetnictvi/subjekty/data/subjekty-data.service';
import {SkladyPolozkyDataService} from '../../../sklady/sklady-polozky/data/sklady-polozky-data.service';
import {PolozkyFakturyDataService} from '../../../ucetnictvi/polozky-faktury/data/polozky-faktury-data.service';
import {DrawerService} from '../../../shared/layout/drawer.service';
import {ActivatedRoute} from '@angular/router';
import {of} from 'rxjs';
import {FormControl, FormGroup} from '@angular/forms';

describe('NovaFakturaComponent - new invoice mode', () => {
  let component: NovaFakturaComponent;
  let fixture: ComponentFixture<NovaFakturaComponent>;

  const mockDialogService = jasmine.createSpyObj('DialogService', ['form', 'alert']);
  const mockBreadcrumbService = { breadcrumbsValue: [] };
  const mockNovaFakturaDataService = jasmine.createSpyObj('NovaFakturaDataService', ['get', 'post']);
  const mockSubjektyDataService = jasmine.createSpyObj('SubjektyDataService', ['getAll']);
  const mockSkladyService = jasmine.createSpyObj('SkladyPolozkyDataService', ['getAll']);
  const mockPolozkyFakturyService = jasmine.createSpyObj('PolozkyFakturyDataService', ['post']);
  const mockDrawerService = jasmine.createSpyObj('DrawerService', ['openWithTemplate', 'close']);

  beforeEach(async () => {
    mockSubjektyDataService.getAll.and.resolveTo({ items: [] });
    mockNovaFakturaDataService.get.and.resolveTo(null);
    mockSkladyService.getAll.and.resolveTo({ items: [] });
    mockPolozkyFakturyService.post.and.resolveTo({});

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
            paramMap: of({ get: () => null }), // žádné id → nová faktura
            snapshot: {
              paramMap: { get: () => null },
              data: {
                basePath: 'ucetnictvi',
                breadcrumb: 'Nová faktura'
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
    mockNovaFakturaDataService.post.calls.reset();
    mockPolozkyFakturyService.post.calls.reset();
    mockDialogService.alert.calls.reset();
  });

  it('should create the component in new invoice mode', async () => {
    await component.ngOnInit();

    expect(component).toBeTruthy();
    expect(component.isNew).toBeTrue();
    expect(component.loaded).toBeTrue();
    expect(component.entityId).toBeFalsy();
  });

});
