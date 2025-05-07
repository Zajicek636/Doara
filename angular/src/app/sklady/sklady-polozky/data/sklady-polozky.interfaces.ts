import {CustomValidator, FormField, FormFieldTypes} from '../../../shared/forms/form.interfaces';

export interface ContainerDto {
  id?: string,
  name: string
  description: string
}

export const CREATE_CONTAINER_FIELDS: Omit<FormField, 'defaultValue'>[] = [
  {
    label: 'Id',
    formControlName: 'ContainerId',
    type: FormFieldTypes.TEXT,
    visible: false,
    defaultValueGetter: (s: ContainerDto) => s.id,
  },
  {
    label: 'Název kontejneru',
    formControlName: 'ContainerName',
    type: FormFieldTypes.TEXT,
    visible: true,
    validator: [{validator: CustomValidator.REQUIRED}],
    defaultValueGetter: (s: ContainerDto) => s.name,
  },
  {
    label: 'Popis',
    formControlName: 'ContainerLabel',
    type: FormFieldTypes.TEXTAREA,
    visible: true,
    validator: [{validator: CustomValidator.REQUIRED}],
    defaultValueGetter: (s: ContainerDto) => s.description,
  }
]
