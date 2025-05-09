import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { By } from '@angular/platform-browser';
import {ConfirmDialogComponent} from '../../shared/dialog/components/confirm-dialog.component';
import {SharedModule} from '../../shared/shared.module';


describe('ConfirmDialogComponent', () => {
  let fixture: ComponentFixture<ConfirmDialogComponent>;
  let component: ConfirmDialogComponent;
  let dialogRefSpy: jasmine.SpyObj<MatDialogRef<ConfirmDialogComponent>>;

  const mockData = {
    title: 'Confirm Deletion',
    message: 'Are you sure you want to delete this?',
    icon: 'delete',
    confirmButton: 'Yes',
    cancelButton: 'No',
    type: 'warning'
  };

  beforeEach(async () => {
    dialogRefSpy = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      imports: [
        SharedModule
      ],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: mockData },
        { provide: MatDialogRef, useValue: dialogRefSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ConfirmDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display title, icon, and message', () => {
    const title = fixture.debugElement.query(By.css('.dialog-header span')).nativeElement;
    const icon = fixture.debugElement.query(By.css('mat-icon')).nativeElement;
    const message = fixture.debugElement.query(By.css('.message')).nativeElement;

    expect(title.textContent).toContain(mockData.title);
    expect(icon.textContent).toContain(mockData.icon);
    expect(message.innerHTML).toContain(mockData.message);
  });

  it('should close with false when cancel is clicked', () => {
    const cancelBtn = fixture.debugElement.query(By.css('button')).nativeElement;
    cancelBtn.click();
    expect(dialogRefSpy.close).toHaveBeenCalledWith(false);
  });

  it('should close with true when confirm is clicked', () => {
    const buttons = fixture.debugElement.queryAll(By.css('button'));
    const confirmBtn = buttons[1].nativeElement; // second button
    confirmBtn.click();
    expect(dialogRefSpy.close).toHaveBeenCalledWith(true);
  });
});
