import {FormComponentResult} from '../../../shared/forms/any-form/any-form.component';
import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';

export interface SubjektDetailDto {
  id: string;
  Name: string;
  Ic: string;
  Dic: string;
  IsVatPayer: boolean;
  AddressDetailDto: AddressDetailDto;
}

export interface AddressDetailDto {
  id:string;
  Street: string;
  City: string;
  PostalCode: string;
  CountryDto: CountryDto;
}

export interface CountryDto {
  id: string;
  Name: string;
  Code: string;
}

export interface SubjektyDialogResult<T = any> {
  subjektBaseResult: FormComponentResult<T>;
  subjektAddressResult: FormComponentResult<T>;
}

export const SUBJEKT_BASE_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    label: 'Id',
    formControlName: 'SubjektId',
    type: FormFieldTypes.TEXT,
    visible: false,
    defaultValueGetter: (s: SubjektDetailDto) => s.id,
  },
  {
    label: 'Jméno',
    formControlName: 'SubjektName',
    visible: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.Name,
    validator: [{ validator: CustomValidator.REQUIRED}],
  },
  {
    label: 'IČ',
    formControlName: 'SubjektIc',
    visible: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.Ic,
    validator: [{ validator: CustomValidator.REQUIRED},{validator: CustomValidator.PATTERN, params: '^\\d{8}$'}]
  },
  {
    label: 'DIČ',
    formControlName: 'SubjektDic',
    visible: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.Dic,
    validator: [{ validator: CustomValidator.REQUIRED }, {validator: CustomValidator.PATTERN, params: '^(CZ\\d{8,10}|SK\\d{10})$'}]
  },
  {
    label: 'Plátce DPH',
    visible: true,
    formControlName: 'SubjektPayer',
    type: FormFieldTypes.LOOKUP,
    defaultValueGetter: (s: SubjektDetailDto) => s.IsVatPayer,
    options: [
      { value: true, displayValue: 'Ano' },
      { value: false, displayValue: 'Ne' }
    ],
    validator: [{ validator: CustomValidator.REQUIRED }]
  }
];

export const SUBJEKT_ADDRESS_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    label: 'AddressId',
    formControlName: 'AddressId',
    visible: false,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.AddressDetailDto.id,
    validator: [{ validator: CustomValidator.REQUIRED}],
  },
  {
    label: 'CountryId',
    formControlName: 'CountryId',
    visible: false,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.AddressDetailDto.CountryDto.id,
    validator: [{ validator: CustomValidator.REQUIRED}],
  },
  {
    label: 'Ulice',
    visible: true,
    formControlName: 'SubjektStreet',
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.Street,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Město',
    visible: true,
    formControlName: 'SubjektCity',
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.City,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'PSČ',
    visible: true,
    formControlName: 'SubjektPSC',
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.PostalCode,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Kód země',
    formControlName: 'SubjektCountryCode',
    visible: true,
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.CountryDto.Name,
    type: FormFieldTypes.LOOKUP,
    options: [
      { value: 'CZ', displayValue: 'CZ' },
      { value: 'SK', displayValue: 'SK' }
    ],
    validator: [{ validator: CustomValidator.REQUIRED }]
  }
];
