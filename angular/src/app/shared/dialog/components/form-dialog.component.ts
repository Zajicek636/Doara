import {Component, Inject, Input} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import { DefaultDialogComponent } from './default-dialog.component';
import {NgClass, NgForOf, NgIf} from '@angular/common';
import {SharedModule} from '../../shared.module';
import {MatButton} from '@angular/material/button';
import {AnyFormComponent, FormComponentResult} from '../../forms/any-form/any-form.component';
import {FormField} from '../../forms/form.interfaces';
import {FormGroup} from '@angular/forms';

@Component({
  selector: 'app-form-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <span>{{ data.title }}</span>
      </div>
      <mat-dialog-content>
        <div class="modal-form">
          <app-any-form [fields]="this.fields" (formChanged)="valueChanged($event)"></app-any-form>
        </div>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button (click)="cancel()">Cancel</button>
        <button mat-button [disabled]="this.isSubmitDisabled" (click)="submit()">Odeslat</button>
      </mat-dialog-actions>
    </div>
  `,
  styleUrls: ['./base-dialog.component.scss'],
  imports: [
    MatDialogContent,
    MatDialogTitle,
    NgClass,
    SharedModule,
    MatDialogActions,
    MatButton,
    AnyFormComponent
  ]
})
export class FormDialogComponent<T> extends DefaultDialogComponent {
  @Input() fields!: FormField[];

  form: FormGroup = new FormGroup({});
  result: FormComponentResult<T> | null = null;

  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<FormDialogComponent<T>>,
  ) {
    super();
    this.fields = this.data.fields;
  }

  public isSubmitDisabled = true;

  submit(): void {
    if(this.form && this.form.valid) {
      this.dialogRef.close(this.result?.data)
    }
  }

  valueChanged(form: FormComponentResult<T>): void {
    this.result = form
    this.form = form.form
    this.isSubmitDisabled = !form.valid
  }

  cancel() {
    this.dialogRef.close();
  }
}
