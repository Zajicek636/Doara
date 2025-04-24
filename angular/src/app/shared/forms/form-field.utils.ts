// form-field.utils.ts

import {FormField} from './form.interfaces';
import {ColumnSetting} from '../table/table/table.settings';

/**
 * Vrátí hodnotu na dané cestě v objektu (podporuje 'foo.bar.baz').
 */
function getNested(obj: any, path: string): any {
  return path.split('.').reduce((acc, key) => acc?.[key], obj);
}

export function populateDefaults<T>(
  fields: FormField[],
  dto: T
): FormField[] {
  return fields.map(f => {
    let value: any = f.defaultValue;
    if (f.defaultValueGetter) {
      value = f.defaultValueGetter(dto);
    }
    return { ...f, defaultValue: value };
  });
}

export function fieldsToColumns<T>(fields: FormField[]): ColumnSetting<T>[] {
  return fields.map(f => ({
    key: f.formControlName,
    header: f.label,
    valueGetter: f.defaultValueGetter
      ? (row: T) => f.defaultValueGetter!(row)
      : (row: any) => row[f.formControlName]
  }));
}
