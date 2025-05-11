import {ColumnSetting} from '../../../shared/table/table/table.settings';
import {InvoiceDto, VatRate, VatRateLabels} from '../../../ucetnictvi/nova-faktura/data/nova-faktura.interfaces';
import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';
import {ContainerItemCreateEditDto} from '../../polozka-kontejneru/data/polozka-kontejneru.interfaces';
import {BaseMaterialIcons} from '../../../../styles/material.icons';

export interface MovementDto {
  id?: string;
  quantity: number;
  movementCategory?: number;
  relatedDocumentId?: string;
}

export enum MovementCategory {
  Unused = 78,
  Reserved = 82,
  Reserved2Use = 83,
  Used = 85
}

export const MovementCategoryLabels: Record<MovementCategory, string> = {
  [MovementCategory.Unused]: 'Přidáno',
  [MovementCategory.Reserved]: 'Rezervováno',
  [MovementCategory.Reserved2Use]: 'Použito',
  [MovementCategory.Used]: 'Použito',

};

export const MOVEMENT_COLUMNS: ColumnSetting<MovementDto>[] = [
  { key: 'movementCategory', header: 'Typ pohybu', valueGetter: r => MovementCategoryLabels[r.movementCategory as MovementCategory] },
  { key: 'quantity', header: 'Množství', valueGetter: r => r.quantity.toString() },
  { key: 'relatedDocument', header: 'Číslo dokladu', valueGetter: r => r.relatedDocumentId ?? '', referenceLabelGetter:() =>"Doklad", isReference: true,},
];

export const ADD_NEW_ITEMS_TO_CONTAINER: Omit<FormField, 'defaultValue'>[] = [
  {
    label: 'Množství',
    formControlName: 'quantity',
    visible: true,
    type: FormFieldTypes.NUMBER,
    validator: [{ validator: CustomValidator.REQUIRED }, {validator: CustomValidator.MIN, params: 1}],
    defaultValueGetter: (item: MovementDto) => item.quantity,
  },
]
