import {Component, Inject} from '@angular/core';
import {AnyFormModule} from '../../shared/forms/any-form/any-form.module';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {NgClass, NgIf} from '@angular/common';
import {DefaultDialogComponent} from '../../shared/dialog/components/default-dialog.component';
import {FormGroup} from '@angular/forms';
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {MatButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import {FormField} from '../../shared/forms/form.interfaces';
import {SubjektyDialogResult} from './data/subjekty.interfaces';
import {MatCard, MatCardModule} from '@angular/material/card';
import {MatDivider} from '@angular/material/divider';
import {BaseMaterialIcons} from '../../../styles/material.icons';

@Component({
  selector: 'app-novy-subjekt-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <mat-icon *ngIf="data.icon">{{ data.icon }}</mat-icon>
        <span>{{ data.title }}</span>
      </div>
      <mat-dialog-content>
        <mat-card appearance="outlined" style="margin: 0.5rem" [class.alert-danger]="!this.baseResult?.valid">
          <mat-card-header>
            <div mat-card-avatar>
              <mat-icon>{{ BaseMaterialIcons.PERSON }}</mat-icon>
            </div>
            <mat-card-title>Základní informace</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <app-any-form [fields]="this.subjektBaseFields" (formChanged)="valueBaseChanged($event)"
                          style="display: flex; flex-direction: column; flex-wrap: wrap; gap: 0.5rem; align-items: stretch; justify-content: space-evenly"
            />
          </mat-card-content>
        </mat-card>

        <mat-card appearance="outlined" style="margin: 0.5rem" [class.alert-danger]="!this.addressResult?.valid">
          <mat-card-header>
            <div mat-card-avatar>
              <mat-icon>{{ BaseMaterialIcons.PERSON_LOCATION }}</mat-icon>
            </div>
            <mat-card-title>Adresa</mat-card-title>
          </mat-card-header>

          <mat-card-content>
            <app-any-form [fields]="this.additionalSubjektFields" (formChanged)="valueAdditionalChanged($event)"
                          style="display: flex; flex-direction: row; flex-wrap: wrap; gap: 0.5rem; align-items: center; justify-content: space-evenly"
            />
          </mat-card-content>
        </mat-card>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button (click)="cancel()">Zrušit</button>
        <button mat-button [disabled]="this.isSubmitDisabled" (click)="submit()">Potvrdit</button>
      </mat-dialog-actions>
    </div>
  `,
  styleUrls: ['../../shared/dialog/components/base-dialog.component.scss'],
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
  ]
})
export class SubjektyModalComponent<T> extends DefaultDialogComponent {
  formBase: FormGroup = new FormGroup({});
  formAdditional: FormGroup = new FormGroup({});

  baseResult?: FormComponentResult<any>;
  addressResult?: FormComponentResult<any>;

  subjektBaseFields: FormField[];
  additionalSubjektFields: FormField[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<SubjektyModalComponent<T>>,
  ) {
    super();

    this.subjektBaseFields = data.subjektBaseFields;
    this.additionalSubjektFields = data.additionalSubjektFields;
    this.updateSubmitState()
  }

  public isSubmitDisabled = true;

  submit(): void {
    if (!this.baseResult || !this.addressResult) return;
    const result: SubjektyDialogResult = {
      subjektBaseResult: this.baseResult,
      subjektAddressResult: this.addressResult
    };
    this.dialogRef.close(result);
  }

  valueBaseChanged(form: FormComponentResult<T>): void {
    this.formBase = form.form
    this.baseResult = form
    this.updateSubmitState();
  }

  valueAdditionalChanged(form: FormComponentResult<T>) {
    this.formAdditional = form.form
    this.addressResult = form
    this.updateSubmitState();
  }

  cancel() {
    this.dialogRef.close();
  }

  private updateSubmitState() {
    const baseValid = this.baseResult?.valid  ?? false;
    const addValid  = this.addressResult?.valid ?? false;
    this.isSubmitDisabled = !(baseValid && addValid);
  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
}
