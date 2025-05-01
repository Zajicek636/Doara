import {Component, Inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {NgClass} from '@angular/common';
import {MatButton} from '@angular/material/button';
import {DefaultDialogComponent} from './default-dialog.component';


@Component({
  selector: 'app-alert-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <span>{{ data.title }}</span>
      </div>

      <mat-dialog-content>
        <p class="message">{{ data.message }}</p>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button [ngClass]="data.type" (click)="close()">OK</button>
      </mat-dialog-actions>
    </div>
  `,
  imports: [
    NgClass,
    MatDialogTitle,
    MatDialogActions,
    MatDialogContent,
    MatButton,
  ],
  styleUrls: ['./base-dialog.component.scss'],
})
export class AlertDialogComponent extends DefaultDialogComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<AlertDialogComponent>
  ) {
    super();
    this.close = this.dialogRef.close.bind(this.dialogRef);
  }

}
