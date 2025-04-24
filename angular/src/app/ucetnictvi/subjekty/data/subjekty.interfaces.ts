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
    label: 'Jméno',
    formControlName: 'SubjektName',
    showInTable: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.Name,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'IČ',
    formControlName: 'SubjektIc',
    showInTable: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.Ic,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'DIČ',
    formControlName: 'SubjektDic',
    showInTable: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.Dic,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Plátce DPH',
    showInTable: true,
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
    label: 'Ulice',
    showInTable: true,
    formControlName: 'SubjektStreet',
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.Street,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Město',
    showInTable: true,
    formControlName: 'SubjektCity',
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.City,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'PSČ',
    showInTable: true,
    formControlName: 'SubjektPSC',
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.PostalCode,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Kód země',
    formControlName: 'SubjektCountryCode',
    showInTable: true,
    defaultValueGetter: (a: SubjektDetailDto) => a.AddressDetailDto.CountryDto.Name,
    type: FormFieldTypes.LOOKUP,
    options: [
      { value: 'CZ', displayValue: 'CZ' },
      { value: 'SK', displayValue: 'SK' }
    ],
    validator: [{ validator: CustomValidator.REQUIRED }]
  }
];
