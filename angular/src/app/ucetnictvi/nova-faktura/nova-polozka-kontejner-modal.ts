import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef,} from '@angular/material/dialog';
import {DefaultDialogComponent} from '../../shared/dialog/components/default-dialog.component';
import {FormGroup} from '@angular/forms';
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {PolozkaKontejneruDataService} from '../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {SkladyPolozkyDataService} from '../../sklady/sklady-polozky/data/sklady-polozky-data.service';
import {SharedModule} from '../../shared/shared.module';
import {ContainerDto} from '../../sklady/sklady-polozky/data/sklady-polozky.interfaces';

@Component({
  selector: 'app-nova-polozka-kontejner-dialog',
  template: `
    <div class="dialog-container">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <mat-icon *ngIf="data.icon">{{ data.icon }}</mat-icon>
        <span>{{ data.title }}</span>
      </div>
      <mat-dialog-content>
        <h1>POLOZKY KONTEJNERU PRO FAKTURU</h1>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button (click)="cancel()">Zrušit</button>
        <button mat-button [disabled]="this.isSubmitDisabled" (click)="submit()">Potvrdit</button>
      </mat-dialog-actions>
    </div>
  `,
  styleUrls: ['../../shared/dialog/components/base-dialog.component.scss'],
  imports: [SharedModule]
})
export class NovaPolozkaKontejnerModal<T> extends DefaultDialogComponent implements OnInit {
  formBase: FormGroup = new FormGroup({});
  formAdditional: FormGroup = new FormGroup({});

  baseResult?: FormComponentResult;
  addressResult?: FormComponentResult;

  contanierData: ContainerDto[] = [];


  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<NovaPolozkaKontejnerModal<T>>,
    private skladyPolozkyDataService: SkladyPolozkyDataService,
    private polozkaKontejneruDataService: PolozkaKontejneruDataService
  ) {
    super();
    this.updateSubmitState()
  }

  ngOnInit(): void {
    this.loadData()
  }

  async loadData(): Promise<void> {
    this.contanierData = (await this.skladyPolozkyDataService.getAll()).items
    console.log(this.contanierData)
  }

  public isSubmitDisabled = true;

  submit(): void {
  /*  if (!this.baseResult || !this.addressResult) return;
    const result: SubjektyDialogResult = {
      subjektBaseResult: this.baseResult,
      subjektAddressResult: this.addressResult
    };
    this.dialogRef.close(result);*/
  }

  valueBaseChanged(form: FormComponentResult): void {
  /*  this.formBase = form.form
    this.baseResult = form
    this.updateSubmitState();*/
  }

  cancel() {
    this.dialogRef.close();
  }

  private updateSubmitState() {
   /* const baseValid = this.baseResult?.valid  ?? false;
    const addValid  = this.addressResult?.valid ?? false;
    this.isSubmitDisabled = !(baseValid && addValid);*/
  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
}
