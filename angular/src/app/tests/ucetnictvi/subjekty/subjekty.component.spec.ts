import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { SubjektyComponent } from '../../../ucetnictvi/subjekty/subjekty.component';
import { SubjektyDataService } from '../../../ucetnictvi/subjekty/data/subjekty-data.service';
import { ZemeDataService } from '../../../ucetnictvi/zeme/data/zeme-data.service';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { DialogService } from '../../../shared/dialog/dialog.service';
import { ActivatedRoute, Router } from '@angular/router';


describe('SubjektyComponent', () => {
  let component: SubjektyComponent;
  let fixture: ComponentFixture<SubjektyComponent>;

  const mockSubjektyDataService = jasmine.createSpyObj('SubjektyDataService', ['post', 'put', 'delete']);
  const mockCountryService = jasmine.createSpyObj('ZemeDataService', ['getAll']);
  const mockDialogService = jasmine.createSpyObj('DialogService', ['open', 'alert', 'confirmAsync']);
  const mockBreadcrumbService = { breadcrumbsValue: [] };
  const mockRouter = {};
  const mockActivatedRoute = {
    snapshot: {
      data: {},
      paramMap: { get: () => null }
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [],
      imports: [SubjektyComponent, RouterTestingModule, HttpClientTestingModule],
      providers: [
        { provide: SubjektyDataService, useValue: mockSubjektyDataService },
        { provide: ZemeDataService, useValue: mockCountryService },
        { provide: DialogService, useValue: mockDialogService },
        { provide: BreadcrumbService, useValue: mockBreadcrumbService },
        { provide: Router, useValue: mockRouter },
        { provide: ActivatedRoute, useValue: mockActivatedRoute }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SubjektyComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add new subjekt from dialog result', async () => {
    const mockSubjekt = {
      id: 'x1',
      name: 'Test s.r.o.',
      ic: '12345678',
      dic: 'CZ12345678',
      isVatPayer: true,
      address: {
        id: 'a1',
        street: 'Testovací 123',
        city: 'Praha',
        postalCode: '11000',
        country: {
          id: 'cz',
          name: 'Česko',
          code: 'CZ'
        }
      }
    };

    const dialogResult = {
      subjektBaseResult: {
        valid: true,
        form: {} as any,
        data: {
          SubjektId: 'x1',
          SubjektName: 'Test s.r.o.',
          SubjektIc: '12345678',
          SubjektDic: 'CZ12345678',
          SubjektPayer: { value: true, displayValue: 'Ano' }
        },
        modified: true
      },
      subjektAddressResult: {
        valid: true,
        form: {} as any,
        data: {
          AddressId: 'a1',
          SubjektStreet: 'Testovací 123',
          SubjektCity: 'Praha',
          SubjektPSC: '11000',
          SubjektCountryCode: { value: 'cz', displayValue: 'Česko' }
        },
        modified: true
      }
    };

    const mockCountry = { id: 'cz', name: 'Česko', code: 'CZ' };
    component.countries = { items: [mockCountry], totalCount: 1 };
    component.tableComponent = {
      dataSource: { data: [] }
    } as any;

    mockDialogService.open.and.resolveTo(dialogResult);
    mockSubjektyDataService.post.and.resolveTo(mockSubjekt);

    await component.addSubjekt();

    expect(component.tableComponent.dataSource.data.length).toBe(1);
    expect(component.tableComponent.dataSource.data[0]).toEqual(mockSubjekt);
  });

  it('should delete subjekt after confirmation', async () => {
    component.chosenElement = {
      id: 'x1',
      name: 'Test s.r.o.',
      ic: '12345678',
      dic: 'CZ12345678',
      isVatPayer: true,
      address: {
        id: 'a1',
        street: 'Testovací 123',
        city: 'Praha',
        postalCode: '11000',
        country: {
          id: 'cz',
          name: 'Česko',
          code: 'CZ'
        }
      }
    };

    const data = [component.chosenElement];
    component.tableComponent = {
      dataSource: { data: [...data] }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(true);
    mockSubjektyDataService.delete.and.resolveTo(null);

    await component.deleteSubjekt();

    expect(component.tableComponent.dataSource.data.length).toBe(0);
    expect(mockSubjektyDataService.delete).toHaveBeenCalledWith('x1');
  });

  it('should not delete subjekt if confirmation is cancelled', async () => {
    component.chosenElement = {
      id: 'x1',
      name: 'Firma s.r.o.',
      ic: '12345678'
    } as any;
    component.tableComponent = {
      dataSource: { data: [component.chosenElement] }
    } as any;

    mockDialogService.confirmAsync.and.resolveTo(false);

    await component.deleteSubjekt();

    expect(component.tableComponent.dataSource.data.length).toBe(1);
    expect(mockSubjektyDataService.delete).not.toHaveBeenCalled();
  });

  it('should not add subjekt when dialog returns null', async () => {
    mockDialogService.open.and.resolveTo(null);

    component.countries = { items: [], totalCount: 0 }; // nutné kvůli .items.map
    component.tableComponent = {
      dataSource: { data: [] }
    } as any;

    await component.addSubjekt();

    expect(component.tableComponent.dataSource.data.length).toBe(0);
  });

  it('should update subjekt after edit dialog', async () => {
    const originalSubjekt = {
      id: 'x1',
      name: 'Původní s.r.o.',
      ic: '12345678',
      dic: 'CZ12345678',
      isVatPayer: false,
      address: {
        id: 'a1',
        street: 'Stará 1',
        city: 'Brno',
        postalCode: '60200',
        country: {
          id: 'cz',
          name: 'Česko',
          code: 'CZ'
        }
      }
    };

    const updatedSubjekt = {
      id: 'x1',
      name: 'Upravený s.r.o.',
      ic: '12345678',
      dic: 'CZ12345678',
      isVatPayer: true,
      address: {
        id: 'a1',
        street: 'Nová 123',
        city: 'Praha',
        postalCode: '11000',
        country: {
          id: 'cz',
          name: 'Česko',
          code: 'CZ'
        }
      }
    };

    const dialogResult = {
      subjektBaseResult: {
        valid: true,
        form: {} as any,
        data: {
          SubjektId: 'x1',
          SubjektName: 'Upravený s.r.o.',
          SubjektIc: '12345678',
          SubjektDic: 'CZ12345678',
          SubjektPayer: { value: true, displayValue: 'Ano' }
        },
        modified: true
      },
      subjektAddressResult: {
        valid: true,
        form: {} as any,
        data: {
          AddressId: 'a1',
          SubjektStreet: 'Nová 123',
          SubjektCity: 'Praha',
          SubjektPSC: '11000',
          SubjektCountryCode: { value: 'cz', displayValue: 'Česko' }
        },
        modified: true
      }
    };

    const mockCountry = { id: 'cz', name: 'Česko', code: 'CZ' };

    component.countries = { items: [mockCountry], totalCount: 1 };
    component.chosenElement = originalSubjekt;
    component.tableComponent = {
      dataSource: {
        data: [originalSubjekt]
      }
    } as any;

    mockDialogService.open.and.resolveTo(dialogResult);
    mockSubjektyDataService.put.and.resolveTo(updatedSubjekt);

    await component.editSubjekt();

    const updated = component.tableComponent.dataSource.data.find(s => s.id === 'x1');
    expect(updated).toEqual(updatedSubjekt);
  });

  it('should correctly map dialog result to SubjektCreateEditDto', () => {
    const mockCountries = [
      { id: 'cz', name: 'Česko', code: 'CZ' },
      { id: 'sk', name: 'Slovensko', code: 'SK' }
    ];

    component.countries = { items: mockCountries, totalCount: 2 };

    const dialogResult = {
      subjektBaseResult: {
        valid: true,
        form: {} as any,
        data: {
          SubjektId: 'x1',
          SubjektName: 'Firma s.r.o.',
          SubjektIc: '12345678',
          SubjektDic: 'CZ12345678',
          SubjektPayer: { value: true, displayValue: 'Ano' }
        },
        modified: true
      },
      subjektAddressResult: {
        valid: true,
        form: {} as any,
        data: {
          AddressId: 'addr1',
          SubjektStreet: 'Ulice 1',
          SubjektCity: 'Brno',
          SubjektPSC: '60200',
          SubjektCountryCode: { value: 'cz', displayValue: 'Česko' }
        },
        modified: true
      }
    };

    const result = (component as any).mapToDto(dialogResult);

    expect(result).toEqual({
      id: 'x1',
      name: 'Firma s.r.o.',
      ic: '12345678',
      dic: 'CZ12345678',
      isVatPayer: true,
      address: {
        id: 'addr1',
        street: 'Ulice 1',
        city: 'Brno',
        postalCode: '60200',
        countryId: 'cz'
      }
    });
  });
});
