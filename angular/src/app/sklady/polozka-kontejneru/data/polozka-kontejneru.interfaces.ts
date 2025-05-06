import {CustomValidator, FormField, FormFieldTypes} from "../../../shared/forms/form.interfaces";

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
    [QuantityType.None]: 'Žádné',
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

export const ContainerItemStateLabels: Record<ContainerItemState, string> = {
    [ContainerItemState.New]: 'Nový',
    [ContainerItemState.Used]: 'Použité',
    [ContainerItemState.Canceled]: 'Zrušeno',
};

export const CONTAINER_ITEM_FIELDS: Omit<FormField, 'defaultValue'>[] = [
    {
      label: 'ID',
      formControlName: 'id',
      visible: false,
      type: FormFieldTypes.TEXT,
      defaultValueGetter: (item: ContainerItemDto) => item.id,
    },
    {
      label: 'Název položky',
      formControlName: 'name',
      visible: true,
      type: FormFieldTypes.TEXT,
      validator: [{ validator: CustomValidator.REQUIRED }],
      defaultValueGetter: (item: ContainerItemDto) => item.name,
    },
    {
      label: 'Popis položky',
      formControlName: 'description',
      visible: true,
      type: FormFieldTypes.TEXTAREA,
      defaultValueGetter: (item: ContainerItemDto) => item.description,
    },
    {
      label: 'Typ množství',
      formControlName: 'quantityType',
      visible: true,
      type: FormFieldTypes.LOOKUP,
      options: Object.values(QuantityType).map(q => ({
          value: q,
          displayValue: QuantityTypeLabels[q as QuantityType]
      })),
      defaultValueGetter: (item: ContainerItemDto) =>  QuantityTypeLabels[item.quantityType as QuantityType],
      validator: [{ validator: CustomValidator.REQUIRED }],
    },
    {
      label: 'Reálná cena',
      formControlName: 'realPrice',
      visible: true,
      type: FormFieldTypes.NUMBER,
      validator: [{ validator: CustomValidator.REQUIRED },{validator: CustomValidator.DECIMAL_PLACES, params: 2}],
      defaultValueGetter: (item: ContainerItemDto) => item.realPrice,
    },
    {
      label: 'Prezentace cena',
      formControlName: 'presentationPrice',
      visible: true,
      type: FormFieldTypes.NUMBER,
      validator: [{validator: CustomValidator.DECIMAL_PLACES, params: 2}],
      defaultValueGetter: (item: ContainerItemDto) => item.presentationPrice,
    },
    {
      label: 'Přirážka (Kč)',
      formControlName: 'markup',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemDto) => item.markup,
    },
    {
      label: 'Přirážka - Sazba  (%)',
      formControlName: 'markupRate',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemDto) => item.markupRate,
    },
    {
      label: 'Sleva (Kč)',
      formControlName: 'discount',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemDto) => item.discount,
    },
    {
      label: 'Sleva - Sazba  (%)',
      formControlName: 'discountRate',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemDto) => item.discountRate,
    },
    {
      label: 'URL nákupu',
      formControlName: 'purchaseUrl',
      visible: true,
      type: FormFieldTypes.TEXT,
      defaultValueGetter: (item: ContainerItemDto) => item.purchaseUrl,
    },
    {
      label: 'Kontejner ID',
      formControlName: 'containerId',
      visible: false,
      type: FormFieldTypes.TEXT,
      defaultValueGetter: (item: ContainerItemDto) => item.containerId,
    }
];

export const CONTAINER_ITEM_CREATE_FIELDS: Omit<FormField, 'defaultValue'>[] = [
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
      label: 'Přirážka (%)',
      formControlName: 'markup',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemCreateEditDto) => item.markup,
      validator: [{ validator: CustomValidator.REQUIRED }],
    },
    {
      label: 'Přirážka - Sazba',
      formControlName: 'markupRate',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemCreateEditDto) => item.markupRate,
    },
    {
      label: 'Sleva (%)',
      formControlName: 'discount',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemCreateEditDto) => item.discount,
    },
    {
      label: 'Sleva - Sazba',
      formControlName: 'discountRate',
      visible: true,
      type: FormFieldTypes.NUMBER,
      defaultValueGetter: (item: ContainerItemCreateEditDto) => item.discountRate,
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
