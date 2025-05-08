import {FormComponentResult} from '../../../shared/forms/any-form/any-form.component';
import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';

export interface SubjektCreateEditDto {
  id?: string;
  name: string;
  ic: string;
  dic: string;
  isVatPayer: boolean;
  address: AddressCreateDto;
}

export interface AddressCreateDto {
  id?: string;
  street: string;
  city: string;
  postalCode: string;
  countryId: string;
}

export interface SubjektDetailDto {
  id: string;
  name: string;
  ic: string;
  dic: string;
  isVatPayer: boolean;
  address: AddressDetailDto;
}

export interface AddressDetailDto {
  id:string;
  street: string;
  city: string;
  postalCode: string;
  country: CountryDto;
}

export interface CountryDto {
  id: string;
  name: string;
  code: string;
}

export interface SubjektyDialogResult<T = any> {
  subjektBaseResult: FormComponentResult;
  subjektAddressResult: FormComponentResult;
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
    defaultValueGetter: (s: SubjektDetailDto) => s.name,
    validator: [{ validator: CustomValidator.REQUIRED}],
  },
  {
    label: 'IČ',
    formControlName: 'SubjektIc',
    visible: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.ic,
    validator: [{validator: CustomValidator.PATTERN, params: '^\\d{8}$'}]
  },
  {
    label: 'DIČ',
    formControlName: 'SubjektDic',
    visible: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.dic,
    validator: [{validator: CustomValidator.PATTERN, params: '^(CZ\\d{8,10}|SK\\d{10})$'}]
  },
  {
    label: 'Plátce DPH',
    visible: true,
    formControlName: 'SubjektPayer',
    type: FormFieldTypes.LOOKUP,
    defaultValueGetter: (s: SubjektDetailDto) => {
      return {value: s.isVatPayer, displayValue: s.isVatPayer ? "Ano" : "Ne"};

    },
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
    defaultValueGetter: (s: SubjektDetailDto) => s.address.id,
  },
  {
    label: 'CountryId',
    formControlName: 'CountryId',
    visible: false,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SubjektDetailDto) => s.address.country.id,
  },
  {
    label: 'Ulice',
    visible: true,
    formControlName: 'SubjektStreet',
    defaultValueGetter: (a: SubjektDetailDto) => a.address.street,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Město',
    visible: true,
    formControlName: 'SubjektCity',
    defaultValueGetter: (a: SubjektDetailDto) => a.address.city,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'PSČ',
    visible: true,
    formControlName: 'SubjektPSC',
    defaultValueGetter: (a: SubjektDetailDto) => a.address.postalCode,
    type: FormFieldTypes.TEXT,
    validator: [
      { validator: CustomValidator.REQUIRED },
      { validator: CustomValidator.PATTERN, params: '^\\d{5}$' }
    ]
  },
  {
    label: 'Kód země',
    formControlName: 'SubjektCountryCode',
    visible: true,
    defaultValueGetter: (a: SubjektDetailDto) => {
      return {
        value: a.address.country.id,
        displayValue: a.address.country.name
      }
    },
    type: FormFieldTypes.LOOKUP,
    validator: [{ validator: CustomValidator.REQUIRED }]
  }
];
