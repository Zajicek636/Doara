import {DefaultDialogComponent} from './default-dialog.component';
import {Component, Inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {MatButton} from '@angular/material/button';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-confirm-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <span>{{ data.title }}</span>
      </div>

      <mat-dialog-content>
        <p class="message">{{ data.message }}</p>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button [ngClass]="data.type" (click)="cancel()">Cancel</button>
        <button mat-button [ngClass]="data.type" (click)="close()">Confirm</button>
      </mat-dialog-actions>
    </div>
  `,
  imports: [
    MatDialogContent,
    MatDialogActions,
    MatButton,
    MatDialogTitle,
    NgClass
  ],
  styleUrls: ['base-dialog.component.scss']
})
export class ConfirmDialogComponent extends DefaultDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {
    super();
    this.close = this.dialogRef.close.bind(this.dialogRef);
  }
  cancel() {
    this.dialogRef.close();
  }
}
