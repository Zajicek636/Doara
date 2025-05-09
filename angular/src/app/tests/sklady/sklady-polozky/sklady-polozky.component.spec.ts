import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {SkladyPolozkyComponent} from '../../../sklady/sklady-polozky/sklady-polozky.component';
import {DialogService} from '../../../shared/dialog/dialog.service';
import {SkladyPolozkyDataService} from '../../../sklady/sklady-polozky/data/sklady-polozky-data.service';
import {RouterTestingModule} from '@angular/router/testing';
import {BreadcrumbService} from '../../../shared/breadcrumb/breadcrumb.service';
import {FormGroup} from '@angular/forms';

describe('SkladyPolozkyComponent', () => {
  let component: SkladyPolozkyComponent;
  let fixture: ComponentFixture<SkladyPolozkyComponent>;
  let dialogService: jasmine.SpyObj<DialogService>;
  let dataService: jasmine.SpyObj<SkladyPolozkyDataService>;

  const dummyContainer = { id: '1', name: 'Test', description: 'Test desc' };

  beforeEach(async () => {
    dialogService = jasmine.createSpyObj('DialogService', ['form', 'alert', 'confirmAsync']);
    dataService = jasmine.createSpyObj('SkladyPolozkyDataService', ['getAll', 'post', 'put', 'delete']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, SkladyPolozkyComponent],
      providers: [
        { provide: DialogService, useValue: dialogService },
        { provide: SkladyPolozkyDataService, useValue: dataService },
        { provide: BreadcrumbService, useValue: { breadcrumbsValue: [] } },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: (key: string) => '1'
              },
              data: {
                basePath: 'sklady',
                breadcrumb: 'Sklady'
              }
            },
            paramMap: of({ get: (key: string) => '1' })
          }
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(SkladyPolozkyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create and load data', async () => {
    dataService.getAll.and.resolveTo({
      items: [dummyContainer],
      totalCount: 1
    });
    await component.loadData();
    expect(component.items.length).toBe(1);
  });

  it('should add new container', async () => {
    dialogService.form.and.resolveTo({
      main_section: {
        valid: true,
        modified: true,
        form: {} as FormGroup,
        data: {
          ContainerId: null,
          ContainerName: 'New',
          ContainerLabel: 'Label'
        }
      }
    });

    dataService.post.and.resolveTo({ id: '2', name: 'New', description: 'Label' });

    await component.addNewContainer();
    expect(component.items.length).toBe(1);
  });

  it('should edit container', async () => {
    component.items = [dummyContainer];

    dialogService.form.and.resolveTo({
      main_section: {
        valid: true,
        modified: true,
        form: {} as FormGroup,
        data: {
          ContainerId: '1',
          ContainerName: 'Updated',
          ContainerLabel: 'Updated desc'
        }
      }
    });

    dataService.put.and.resolveTo(undefined);

    await component.handleEditClick(dummyContainer);
    expect(component.items[0].name).toBe('Updated');
    expect(component.items[0].description).toBe('Updated desc');
  });

  it('should delete container', async () => {
    component.items = [dummyContainer];
    dialogService.confirmAsync.and.resolveTo(true);
    dataService.delete.and.resolveTo(undefined);

    await component.handleDeleteClick(dummyContainer);
    expect(component.items.length).toBe(0);
  });

  it('should skip delete if not confirmed', async () => {
    component.items = [dummyContainer];
    dialogService.confirmAsync.and.resolveTo(false);

    await component.handleDeleteClick(dummyContainer);
    expect(dataService.delete).not.toHaveBeenCalled();
  });

  it('should show alert on delete failure', async () => {
    component.items = [dummyContainer];
    dialogService.confirmAsync.and.resolveTo(true);
    dataService.delete.and.rejectWith({ error: { error: { message: 'Fail' } } });

    await component.handleDeleteClick(dummyContainer);
    expect(dialogService.alert).toHaveBeenCalled();
  });
});
