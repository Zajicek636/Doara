import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {AlertDialogComponent} from './components/alert-dialog.component';
import {DefaultDialogComponent} from './components/default-dialog.component';
import {
  AlertDialogParams,
  ConfirmDialogParams,
  DialogType,
  DynamicDialogResult,
  FormDialogParams
} from './dialog.interfaces';
import {ConfirmDialogComponent} from './components/confirm-dialog.component';
import {FormDialogComponent} from './components/form-dialog.component';

@Injectable({ providedIn: 'root' })
export class DialogService {
  constructor(private dialog: MatDialog) {}

  open<T extends DefaultDialogComponent>(component: new (...args: any[]) => T, data?: any): Promise<any> {
    const dialogRef = this.dialog.open(component, {
      data,
      width: '90vw',
      maxWidth: '800px',
      minWidth: '300px',
      panelClass: 'custom-dialog'
    });

    return dialogRef.afterClosed().toPromise();
  }

  async alert(params: AlertDialogParams): Promise<void> {
    return this.open(AlertDialogComponent, {
      title: params.title,
      message: params.message,
      type: params.dialogType,
    });
  }

  async confirmAsync(params: ConfirmDialogParams): Promise<boolean> {
    const result = await this.open(ConfirmDialogComponent, {
      title: params.title ?? "Potvrdit operaci",
      icon: params.icon,
      message: params.message,
      type: params.dialogType,
      cancelButton: params.cancelButton ?? "Zrušit",
      confirmButton: params.confirmButton ?? "Potvrdit"
    });
    return result;
  }

  async form<T>(params: FormDialogParams): Promise<DynamicDialogResult> {
    return this.open(FormDialogComponent, {
      headerIcon: params.headerIcon,
      title: params.title,
      fields: params.sections,
      type: params.type ?? DialogType.SUCCESS,
    });
  }
}
