import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {AlertDialogComponent} from './components/alert-dialog.component';
import {DefaultDialogComponent} from './components/default-dialog.component';
import {AlertDialogParams, ConfirmDialogParams, DialogType, FormDialogParams} from './dialog.interfaces';
import {ConfirmDialogComponent} from './components/confirm-dialog.component';
import {FormDialogComponent} from './components/form-dialog.component';

@Injectable({ providedIn: 'root' })
export class DialogService {
  constructor(private dialog: MatDialog) {}

  open<T extends DefaultDialogComponent>(component: new (...args: any[]) => T, data?: any): Promise<any> {
    const dialogRef = this.dialog.open(component, {
      data,
      width: '30%',
      height: 'auto',
      maxWidth: '100%',
      panelClass: 'custom-dialog',
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
      title: params.title,
      message: params.message,
      type: params.dialogType,
      cancelButton: params.cancelButton,
      confirmButton: params.confirmButton
    });
    return result;
  }

  async form<T>(params: FormDialogParams): Promise<T | undefined> {
    return this.open(FormDialogComponent, {
      title: params.title,
      fields: params.fields,
      type: DialogType.ALERT,
    });
  }
}
