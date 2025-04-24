import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';
import {SubjektDetailDto} from '../../subjekty/data/subjekty.interfaces';

export interface SeznamFakturDto {
  id: string;
  subjektname: string;
  subjektIco: string
}


export const SEZNAM_FAKTUR_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    label: 'Jméno',
    formControlName: 'SubjektName',
    showInTable: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SeznamFakturDto) => s.subjektname,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
  {
    label: 'Ičo',
    formControlName: 'SubjektIco',
    showInTable: true,
    type: FormFieldTypes.TEXT,
    defaultValueGetter: (s: SeznamFakturDto) => s.subjektIco,
    validator: [{ validator: CustomValidator.REQUIRED }]
  },
];
