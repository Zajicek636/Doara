import {CustomValidator, FormField, FormFieldTypes} from "../../../shared/forms/form.interfaces";
import {MovementDto} from "../../sklady-editace/data/sklady-pohyby-polozky.interfaces";

export interface ContainerItemCreateEditDto {
  id?: string;
  quantityType?: QuantityType;
  name?: string;
  description?: string;
  purchaseUrl?: string;
  realPrice?: number;
  markup?: number;
  markupRate?: number;
  discount?: number;
  discountRate?: number;
  containerId: string;
}

export interface ContainerItemUpdateInputDto { //todo dodelat az bude na BE
    id: string;
    name: string;
}

export interface ContainerItemDto {
    id?: string;
    quantityType?: QuantityType;
    name?: string;
    description?: string;
    purchaseUrl?: string;
    realPrice?: number;
    presentationPrice?: number;
    markup?: number;
    markupRate?: number;
    discount?: number;
    discountRate?: number;
    containerId: string;
    available?: number,
    onHand?: number,
    reserved?: number,
    movements?: MovementDto[];
}

export enum ContainerItemState {
    New = 'N',
    Used = 'U',
    Canceled = 'C'
}

export enum QuantityType {
    Unknown = 88, //X
    None = 78,

    Milligrams = 107,
    Grams = 103,
    Kilograms = 75,

    Millimeters = 109,
    Centimeters = 99,
    Decimeters = 100,
    Meters = 77,

    Milliliters = 108,
    Liters = 76
}

export const QuantityTypeLabels: Record<QuantityType, string> = {
    [QuantityType.Unknown]: 'Neznámé',
    [QuantityType.None]: 'Kusy',
    [QuantityType.Milligrams]: 'Miligramy',
    [QuantityType.Grams]: 'Gramy',
    [QuantityType.Kilograms]: 'Kilogramy',
    [QuantityType.Millimeters]: 'Milimetry',
    [QuantityType.Centimeters]: 'Centimetry',
    [QuantityType.Decimeters]: 'Decimetery',
    [QuantityType.Meters]: 'Metry',
    [QuantityType.Milliliters]: 'Mililitry',
    [QuantityType.Liters]: 'Litry',
};

export const CONTAINER_ITEM_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    label: 'Název položky',
    formControlName: 'name',
    visible: true,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.REQUIRED },{ validator: CustomValidator.MAX, params: 255}],
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.name,
  },
  {
    label: 'Popis položky',
    formControlName: 'description',
    visible: true,
    type: FormFieldTypes.TEXTAREA,
    validator: [{validator: CustomValidator.REQUIRED},{ validator: CustomValidator.MAX, params: 255}],
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.description,
  },
  {
    label: 'Typ množství',
    formControlName: 'quantityType',
    visible: true,
    type: FormFieldTypes.LOOKUP,
    options: Object.values(QuantityType)
    .filter(value => typeof value === 'number') // ← jen číselné hodnoty
    .map(q => ({
      value: q,
      displayValue: QuantityTypeLabels[q as QuantityType]
    })),
    defaultValueGetter: (item: ContainerItemDto) =>  item.quantityType,
    validator: [{ validator: CustomValidator.REQUIRED }],
  },
  {
    label: 'Reálná cena',
    formControlName: 'realPrice',
    visible: true,
    type: FormFieldTypes.NUMBER,
    validator: [{ validator: CustomValidator.REQUIRED }],
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.realPrice,
  },
  {
    label: 'Přirážka (Kč)',
    formControlName: 'markup',
    visible: true,
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.markup,
  },
  {
    label: 'Sleva (%)',
    formControlName: 'discountRate',
    visible: true,
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.discountRate,
  },
  {
    label: 'Přirážka (%)',
    formControlName: 'markupRate',
    visible: true,
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.markupRate,
  },
  {
    label: 'Sleva (Kč)',
    formControlName: 'discount',
    visible: true,
    type: FormFieldTypes.NUMBER,
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.discount,
  },
  {
    label: 'URL nákupu',
    formControlName: 'purchaseUrl',
    visible: true,
    type: FormFieldTypes.TEXT,
    validator: [{ validator: CustomValidator.MAX, params: 2048}],
    defaultValueGetter: (item: ContainerItemCreateEditDto) => item.purchaseUrl,
  }
];
