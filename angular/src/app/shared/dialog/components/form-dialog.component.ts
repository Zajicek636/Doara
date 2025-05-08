import {Component, EventEmitter, Inject, Input, Output} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import { DefaultDialogComponent } from './default-dialog.component';
import {NgClass, NgForOf, NgIf,} from '@angular/common';
import {SharedModule} from '../../shared.module';
import {MatButton} from '@angular/material/button';
import { FormComponentResult} from '../../forms/any-form/any-form.component';
import {FormField} from '../../forms/form.interfaces';
import {FormGroup} from '@angular/forms';
import {AnyFormModule} from '../../forms/any-form/any-form.module';
import {MatIcon} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {BaseMaterialIcons} from '../../../../styles/material.icons';
import {FormDialogParams, FormSection} from '../dialog.interfaces';
@Component({
  selector: 'app-form-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <mat-icon *ngIf="data.headerIcon">{{ data.headerIcon }}</mat-icon>
        <span>{{ data.title }}</span>
      </div>
      <mat-dialog-content>
        <mat-card *ngFor="let section of data.fields" appearance="outlined" style="margin: 0.5rem" [class.alert-danger]="!isSectionValid(section.sectionId)">
          <mat-card-header>
            <div mat-card-avatar>
              <mat-icon>{{ section.headerIcon }}</mat-icon>
            </div>
            <mat-card-title>{{ section.sectionTitle }}</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <app-any-form
              [fields]="section.fields"
              (formReady)="onFormReady(section.sectionId, $event)"
              (formChanged)="valueChanged($event, section.sectionId)"
              style="display: flex; flex-direction: column; flex-wrap: wrap; gap: 0.5rem; align-items: stretch; justify-content: space-evenly"
            />
          </mat-card-content>
        </mat-card>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button (click)="cancel()">Cancel</button>
        <button mat-button [disabled]="isSubmitDisabled" (click)="submit()">Odeslat</button>
      </mat-dialog-actions>
    </div>
  `,
  styleUrls: ['./base-dialog.component.scss'],
  imports: [
    AnyFormModule,
    MatDialogContent,
    MatDialogTitle,
    MatDialogActions,
    MatButton,
    MatIcon,
    NgIf,
    NgClass,
    MatCardModule,
    NgForOf,
  ]
})
export class FormDialogComponent extends DefaultDialogComponent {
  @Input() fields!: FormSection[];
  @Output() formReady = new EventEmitter<{ sectionId: string, form: FormGroup }>();

  onFormReady(sectionId: string, form: FormGroup): void {
    this.formReady.emit({ sectionId, form });
  }

  form: FormGroup = new FormGroup({});
  result: any = {};
  sectionValidity: { [sectionId: string]: boolean } = {};

  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<FormDialogComponent>,
  ) {
    super();
    this.fields = this.data.fields;
  }

  public isSubmitDisabled = true;

  submit(): void {
    if (this.form && this.form.valid) {
      this.fields.forEach(section => {
        if (section.sectionId && this.result[section.sectionId]) {
          this.result[section.sectionId] = this.result[section.sectionId];
        }
      });
      this.dialogRef.close(this.result);
    }
  }

  valueChanged(form: FormComponentResult, sectionId: string): void {
    this.result[sectionId] = form;
    this.sectionValidity[sectionId] = form.valid;
    this.updateSubmitButtonState();
  }

  private updateSubmitButtonState(): void {
    this.isSubmitDisabled = !Object.values(this.sectionValidity).every(isValid => isValid);
  }

  isSectionValid(sectionId: string): boolean {
    return this.sectionValidity[sectionId] !== undefined ? this.sectionValidity[sectionId] : false; // defaultně platné, pokud není validita nastavena
  }

  cancel() {
    this.dialogRef.close();
  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
}
