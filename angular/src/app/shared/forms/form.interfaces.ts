export enum FormFieldTypes {
  TEXT = 'string',
  NUMBER = 'number',
  BOOLEAN = 'boolean',
  AUTOCOMPLETE = 'autocomplete',
  LOOKUP = 'lookup',
  PASSWORD = 'password',
  TEXTAREA = 'textarea',
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

export interface FormSelect {
  value: any;
  displayValue: string;
}

export interface FormGroupedSelect {
  groupName: string;
  val: FormSelect[]
}

export interface FormField {
  formControlName: string;
  multipleSelect?: boolean;
  options?: FormSelect[] | FormGroupedSelect[];
  defaultValue?: any;
  label: string;
  type: FormFieldTypes;
  validator?: ValidatorConfig[];
}
