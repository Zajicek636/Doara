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
import {AnyFormComponent} from '../../forms/any-form/any-form.component';
import {FormField} from '../../forms/form.interfaces';

@Component({
  selector: 'app-form-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header rounded-bottom-5 shadow-sm" [ngClass]="data.type">
        <span>{{ data.title }}</span>
      </div>
      <mat-dialog-content>
        <app-any-form [fields]="this.fields">
        </app-any-form>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button (click)="cancel()">Cancel</button>
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
export class FormDialogComponent extends DefaultDialogComponent {
  @Input() fields!: FormField[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<FormDialogComponent>,
  ) {
    super();
    this.fields = this.data.fields;
  }

  cancel() {
    this.dialogRef.close();
  }
}
