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
import {NgClass, NgIf} from '@angular/common';

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
        <button *ngIf="data.cancelButton" mat-button [ngClass]="data.type" (click)="onCancel()">{{data.cancelButton}}</button>
        <button *ngIf="data.confirmButton" mat-button [ngClass]="data.type" (click)="onConfirm()">{{data.confirmButton}}</button>
      </mat-dialog-actions>
    </div>
  `,
  imports: [
    MatDialogContent,
    MatDialogActions,
    MatButton,
    MatDialogTitle,
    NgClass,
    NgIf
  ],
  styleUrls: ['base-dialog.component.scss']
})
export class ConfirmDialogComponent extends DefaultDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {
    super();
  }
  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
