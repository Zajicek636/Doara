import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import {AlertDialogComponent} from '../../shared/dialog/components/alert-dialog.component';


describe('AlertDialogComponent', () => {
  let component: AlertDialogComponent;
  let fixture: ComponentFixture<AlertDialogComponent>;
  let dialogRefSpy: jasmine.SpyObj<MatDialogRef<AlertDialogComponent>>;

  const mockData = {
    title: 'Test Title',
    message: 'This is a test message.',
    type: 'warning'
  };

  beforeEach(async () => {
    dialogRefSpy = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      imports: [
        AlertDialogComponent,
        MatButtonModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: mockData },
        { provide: MatDialogRef, useValue: dialogRefSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AlertDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render title and message', () => {
    const titleEl = fixture.debugElement.query(By.css('.dialog-header span')).nativeElement;
    const messageEl = fixture.debugElement.query(By.css('.message')).nativeElement;

    expect(titleEl.textContent).toContain(mockData.title);
    expect(messageEl.textContent).toContain(mockData.message);
  });

  it('should apply class based on type', () => {
    const headerEl = fixture.debugElement.query(By.css('.dialog-header'));
    expect(headerEl.nativeElement.classList).toContain('warning');
  });

  it('should close dialog on OK click', () => {
    const okButton = fixture.debugElement.query(By.css('button'));
    okButton.nativeElement.click();
    expect(dialogRefSpy.close).toHaveBeenCalled();
  });
});
