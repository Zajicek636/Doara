import {FormGroup} from '@angular/forms';
import {FormField} from '../forms/form.interfaces';

export enum DialogType {
  WARNING = "alert-warning",
  ERROR = "alert-danger",
  ALERT = "alert-alert",
  SUCCESS = "alert-success",
}

export interface DialogParams {
  title: string,
  message: string,
  dialogType: DialogType
  icon?: string,
}

export interface AlertDialogParams extends DialogParams {

}

export interface ConfirmDialogParams extends DialogParams {
  cancelButton: string
  confirmButton: string
}

export interface FormDialogParams {
  title: string,
  fields: FormField[]
}
