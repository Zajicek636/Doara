export enum DialogType {
  WARNING = "alert-warning",
  ERROR = "alert-danger",
  ALERT = "alert-alert",
  SUCCESS = "alert-success",
}

export interface AlertDialogParams {
  title: string,
  message: string,
  dialogType: DialogType
}
