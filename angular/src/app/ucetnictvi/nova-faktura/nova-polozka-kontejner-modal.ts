import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef,} from '@angular/material/dialog';
import {DefaultDialogComponent} from '../../shared/dialog/components/default-dialog.component';
import {FormGroup, Validators} from '@angular/forms';
import {FormComponentResult} from '../../shared/forms/any-form/any-form.component';
import {BaseMaterialIcons} from '../../../styles/material.icons';
import {PolozkaKontejneruDataService} from '../../sklady/polozka-kontejneru/data/polozka-kontejneru-data.service';
import {SkladyPolozkyDataService} from '../../sklady/sklady-polozky/data/sklady-polozky-data.service';
import {SharedModule} from '../../shared/shared.module';
import {ContainerDto} from '../../sklady/sklady-polozky/data/sklady-polozky.interfaces';
import {ADD_POLOZKA_FROM_CONTAINER, VAT_RATE_PERCENT, VatRate, VatRateLabels} from './data/nova-faktura.interfaces';
import {FormField, FormFieldTypes} from '../../shared/forms/form.interfaces';
import {FormService} from '../../shared/forms/form.service';
import {InvoiceItemDto} from '../polozky-faktury/data/polozky-faktury.interfaces';
import {DialogService} from '../../shared/dialog/dialog.service';
import {DialogType} from '../../shared/dialog/dialog.interfaces';
import {MatTableModule} from '@angular/material/table';
import {round} from '../../shared/forms/form-field.utils';

@Component({
  selector: 'app-nova-polozka-kontejner-dialog',
  template: `
    <div class="dialog-container" *ngIf="loadded">
      <div mat-dialog-title class="dialog-header shadow-sm" [ngClass]="data.type">
        <mat-icon *ngIf="data.icon">{{ data.icon }}</mat-icon>
        <span>{{ data.title }}</span>
      </div>
      <mat-dialog-content>
        <ng-container [formGroup]="this.formBase">
          <ng-container *ngFor="let field of formFields">
            <ng-container *ngIf="field.visible">

              <app-autocomplete-field
                *ngIf="field.type === formFieldTypes.AUTOCOMPLETE"
                style="width: 100%"
                [field]="field"
                [form]="this.formBase">
              </app-autocomplete-field>

              <app-lookup-field
                *ngIf="field.type === formFieldTypes.LOOKUP"
                style="width: 100%"
                [field]="field"
                [form]="this.formBase">
              </app-lookup-field>

              <app-number-field
                *ngIf="field.type === formFieldTypes.NUMBER"
                style="width: 100%"
                [field]="field"
                [form]="this.formBase">
              </app-number-field>

            </ng-container>
          </ng-container>
        </ng-container>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button (click)="cancel()">Zrušit</button>
        <button mat-button [disabled]="this.formBase.invalid" (click)="submit()">Potvrdit</button>
      </mat-dialog-actions>
    </div>
  `,
  styleUrls: ['../../shared/dialog/components/base-dialog.component.scss'],
  imports: [SharedModule, MatTableModule]
})
export class NovaPolozkaKontejnerModal<T> extends DefaultDialogComponent implements OnInit {
  formBase: FormGroup = new FormGroup({});
  formFields: FormField[]= [];
  containers: ContainerDto[] = [];
  containerOptions:any[] = [];
  containerItemsById: InvoiceItemDto[] = [];
  loadded = false;




  constructor(
    @Inject(MAT_DIALOG_DATA) public override data: any,
    private dialogRef: MatDialogRef<NovaPolozkaKontejnerModal<T>>,
    private skladyPolozkyDataService: SkladyPolozkyDataService,
    private polozkaKontejneruDataService: PolozkaKontejneruDataService,
    private formService: FormService,
    private dialogService: DialogService,
  ) {
    super();
    this.containers = this.data.containers.items;
    this.updateSubmitState()
  }

  ngOnInit(): void {
    this.initForm()
    this.loadded = true;
  }

  async initForm() {
    this.formFields = ADD_POLOZKA_FROM_CONTAINER
    this.formBase = this.formService.createForm(this.formFields);


    this.containerOptions = this.containers.map((s: ContainerDto) => ({
      value: s.id,
      displayValue: `${s.name}`
    }));

    this.formFields = this.formFields.map(f => {
      if (f.formControlName === 'containerId') {
        return { ...f, visible: true, options: this.containerOptions };
      }
      if (f.formControlName === 'vatRate' || f.formControlName === 'quantity') {
        return { ...f, visible: false };
      }
      return f;
    });
    this.formBase.get('containerId')?.valueChanges.subscribe(x => {
      if (typeof x === 'string' && x !== '') {
        return;
      }

      if (!x) {
        this.formBase.get('containerItem')?.setValue(null);
        this.updateFormFieldVisibility('containerItem', false);
        this.updateFormFieldVisibility('vatRate', false);
        this.updateFormFieldVisibility('quantity', false);
        return;
      }

      const containerId = x.value;
      this.loadContainerItemsById(containerId);
      this.updateFormFieldVisibility('containerItem', true);
    });

    this.formBase.get('containerItem')?.valueChanges.subscribe(itemId => {
      const selectedItem: any = this.containerItemsById.find(x=> x.containerItemId == itemId.value)
      this.updateFormFieldVisibility('vatRate', !!itemId);
      this.updateFormFieldVisibility('quantity', !!itemId);

      const quantityCtrl = this.formBase.get('quantity');
      if (selectedItem && quantityCtrl) {
        quantityCtrl.setValidators([
          Validators.required,
          Validators.min(1),
          Validators.max(selectedItem.available || 0)
        ]);
        quantityCtrl.updateValueAndValidity();
      }
    });
  }

  async loadContainerItemsById(containerId: string) {
    if (!containerId) {return}

    try {
      const res = await this.polozkaKontejneruDataService.getAllWithDetail({ContainerId: containerId})

      this.containerItemsById = res.items.map(item => ({
        containerItemId: item.id,
        description: `${item.name} - ${item.description}`,
        quantity: 0,
        unitPrice: item.presentationPrice,
        netAmount: 0,
        vatRate: 0,
        vatAmount: 0,
        grossAmount: 0,
        available: item.available ?? 0
      } as InvoiceItemDto));

      const options = this.containerItemsById.map(item => ({
        value: item.containerItemId,
        displayValue: item.description
      }));

      this.formFields = this.formFields.map(f => {
        if (f.formControlName === 'containerItem') {
          return {...f, options};
        }
        return f;
      });

    } catch (e: any) {
      await this.dialogService.alert({
        title: "Chyba",
        message: e.error?.error?.message ?? 'Nepodařilo se načíst položky z kontejneru',
        dialogType: DialogType.ERROR
      });
    }
  }

  updateFormFieldVisibility(formControl:string, val: boolean = true) {
    this.formFields = this.formFields.map(f => {
      if (f.formControlName === formControl) {
        return { ...f, visible: val };
      }
      return f;
    });
  }

  submit(): void {
    const formValue = this.formBase.value;
    const selectedItem = this.containerItemsById.find(
      item => item.containerItemId === formValue.containerItem?.value
    );

    if (!selectedItem) {
      this.dialogService.alert({
        title: 'Chyba',
        message: 'Položka nebyla nalezena.',
        dialogType: DialogType.ERROR
      });
      return;
    }

    const quantity = parseFloat(formValue.quantity) || 0;
    const unitPrice = parseFloat(selectedItem.unitPrice?.toString() || '0');
    const vatRate = VAT_RATE_PERCENT[formValue.vatRate?.value as VatRate];

    const netAmount = unitPrice * quantity;
    const vatAmount = netAmount * (vatRate / 100);
    const grossAmount = netAmount + vatAmount;

    const invoiceItem: InvoiceItemDto = {
      id: undefined,
      containerItemId: selectedItem.containerItemId,
      description: selectedItem.description,
      quantity: quantity,
      unitPrice: unitPrice,
      vatRate: formValue.vatRate?.value,
      netAmount: Math.round(netAmount * 100) / 100,
      vatAmount: Math.round(vatAmount * 100) / 100,
      grossAmount: Math.round(grossAmount * 100) / 100,
    };

    this.dialogRef.close(invoiceItem);
  }


  cancel() {
    this.dialogRef.close();
  }

  private updateSubmitState() {

  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
  protected readonly formFieldTypes = FormFieldTypes;
}
