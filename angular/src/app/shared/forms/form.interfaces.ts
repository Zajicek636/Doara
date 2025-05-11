export enum FormFieldTypes {
  TEXT = 'string',
  NUMBER = 'number',
  BOOLEAN = 'boolean',
  RADIO = 'radiobutton',
  AUTOCOMPLETE = 'autocomplete',
  LOOKUP = 'lookup',
  PASSWORD = 'password',
  TEXTAREA = 'textarea',
  DATE = 'date'
}

export enum CustomValidator {
  REQUIRED = 'required',
  MIN = 'min',
  MAX = 'max',
  EMAIL = 'email',
  PATTERN = 'pattern',
  MIN_DATE = 'minDate',
  MAX_DATE = 'maxDate',
  DECIMAL_PLACES = 'decimalPlaces'
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

export interface  FormField {
  formControlName: string;
  multipleSelect?: boolean;
  options?: FormSelect[] | FormGroupedSelect[];
  defaultValue?: any;
  label: string;
  type: FormFieldTypes;
  validator?: ValidatorConfig[];
  visible?: boolean;
  //helpingy pro default values z dto pro table a mapovani a asynchronni operace
  defaultValueGetter: (dto: any) => any;
  isReference?: boolean; // nově
  referenceLabel?: string | ((dto: any) => string);
}
