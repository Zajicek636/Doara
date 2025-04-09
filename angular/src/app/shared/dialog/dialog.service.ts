import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {AlertDialogComponent} from './components/alert-dialog.component';
import {DefaultDialogComponent} from './components/default-dialog.component';
import {AlertDialogParams} from './dialog.interfaces';
import {BaseDialogComponent} from './base-dialog/base-dialog.component';


@Injectable({ providedIn: 'root' })
export class DialogService {
  constructor(private dialog: MatDialog) {}

  open<T extends DefaultDialogComponent>(component: new (...args: any[]) => T, data?: any): Promise<any> {
    const dialogRef = this.dialog.open(component, {
      data,
      width: '400px',
      panelClass: 'custom-dialog',
    });

    return dialogRef.afterClosed().toPromise();
  }

  alert(params: AlertDialogParams): Promise<void> {
    return this.open(AlertDialogComponent, {
      title: params.title,
      message: params.message,
      type: params.dialogType,
    });
  }

  confirmAsync(message: string, title: string = 'Potvrzení'): Promise<boolean> {
    return this.open(BaseDialogComponent, {
      title,
      message,
      type: 'alert-success',
      hasCancelButton: true,
      cancelButtonText: 'Cancel',
      confirmButtonText: 'Confirm',
    }).then(result => !!result);
  }

 /* form<T>(component: new (...args: any[]) => DefaultDialogComponent<any, T>, data?: any): Promise<T | undefined> {
    return this.open(component, data);
  }*/
}
