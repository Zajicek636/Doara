import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import {FormDialogComponent} from '../../shared/dialog/components/form-dialog.component';
import {SharedModule} from '../../shared/shared.module';

describe('FormDialogComponent', () => {
  let component: FormDialogComponent;
  let fixture: ComponentFixture<FormDialogComponent>;
  let dialogRefSpy: jasmine.SpyObj<MatDialogRef<FormDialogComponent>>;

  const mockFields = [
    {
      sectionId: 'section1',
      sectionTitle: 'Section 1',
      headerIcon: 'person',
      fields: []
    },
    {
      sectionId: 'section2',
      sectionTitle: 'Section 2',
      headerIcon: 'info',
      fields: []
    }
  ];

  const mockData = {
    title: 'Test Dialog',
    headerIcon: 'edit',
    type: 'info',
    fields: mockFields
  };

  beforeEach(async () => {
    dialogRefSpy = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      imports: [SharedModule],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: mockData },
        { provide: MatDialogRef, useValue: dialogRefSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(FormDialogComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
    await fixture.whenStable();
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render all form sections', () => {
    const cards = fixture.nativeElement.querySelectorAll('mat-card');
    expect(cards.length).toBe(2); // section1 and section2
  });

  it('should call dialogRef.close() on cancel()', () => {
    component.cancel();
    expect(dialogRefSpy.close).toHaveBeenCalled();
  });

  it('should disable submit if section is invalid', () => {
    component.valueChanged({ valid: false, data: {}, modified: false, form: new FormGroup({}) }, 'section1');
    fixture.detectChanges();
    expect(component.isSubmitDisabled).toBeTrue();
  });

  it('should enable submit if all sections valid and submit form', () => {
    // Simulate 2 valid sections
    component.valueChanged({ valid: true, data: { x: 1 }, modified: true, form: new FormGroup({}) }, 'section1');
    component.valueChanged({ valid: true, data: { y: 2 }, modified: true, form: new FormGroup({}) }, 'section2');
    fixture.detectChanges();

    expect(component.isSubmitDisabled).toBeFalse();
    component.submit();

    expect(dialogRefSpy.close).toHaveBeenCalledWith(component.result);
  });

  it('should emit formReady event', () => {
    spyOn(component.formReady, 'emit');
    const dummyForm = new FormGroup({});
    component.onFormReady('section1', dummyForm);

    expect(component.formReady.emit).toHaveBeenCalledWith({ sectionId: 'section1', form: dummyForm });
  });
});
