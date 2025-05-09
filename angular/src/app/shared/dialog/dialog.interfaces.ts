import {FormGroup} from '@angular/forms';
import {FormField} from '../forms/form.interfaces';
import {FormComponentResult} from '../forms/any-form/any-form.component';

export enum DialogType {
  WARNING = "alert-warning",
  ERROR = "alert-danger",
  ALERT = "alert-alert",
  SUCCESS = "alert-success",
}

export interface DialogParams {
  title?: string,
  message: string,
  dialogType: DialogType
  icon?: string,
}

export interface AlertDialogParams extends DialogParams {

}

export interface ConfirmDialogParams extends DialogParams {
  cancelButton?: string
  confirmButton?: string
}

export interface FormDialogParams {
  headerIcon?: string,
  title: string,
  sections: FormSection[],
  type: DialogType,
}

export interface FormSection {
  sectionId:string
  headerIcon?: string,
  sectionTitle: string,
  fields: FormField[],
}

export type DynamicDialogResult = {
  [sectionId: string]: FormComponentResult;
};
