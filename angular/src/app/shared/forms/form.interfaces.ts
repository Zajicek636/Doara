export enum FormFieldTypes {
  TEXT = 'string',
  NUMBER = 'number',
  BOOLEAN = 'boolean',
  AUTOCOMPLETE = 'autocomplete',
  LOOKUP = 'lookup',
  PASSWORD = 'password',
}

export enum CustomValidator {
  REQUIRED = 'required',
  MIN = 'min',
  MAX = 'max',
  EMAIL = 'email',
  PATTERN = 'pattern',
}

export interface ValidatorConfig {
  validator: CustomValidator;
  params?: any;
}

export interface FormField {
  formControlName: string;
  label: string;
  type: FormFieldTypes;
  defaultValue?: any;
  validator: ValidatorConfig[];
}
