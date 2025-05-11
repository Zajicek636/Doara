// form-field.utils.ts

import {FormField, FormGroupedSelect, FormSelect} from './form.interfaces';
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
    // 1) získej surovou hodnotu
    let rawValue: any = f.defaultValue;
    if (f.defaultValueGetter) {
      rawValue = f.defaultValueGetter(dto);
    }

    // 2) pokud má options, najdi odpovídající FormSelect
    let defaultValue = rawValue;
    if (f.options) {
      // sjednotíme grouped i flat options
      const opts: FormSelect[] = Array.isArray(f.options) &&
      (f.options as any)[0]?.groupName
        ? (f.options as FormGroupedSelect[]).flatMap(g => g.val)
        : (f.options as FormSelect[]);
      const match = opts.find(o => o.value === rawValue);
      if (match) {
        defaultValue = match;
      }
    }

    return { ...f, defaultValue };
  });
}

export function fieldsToColumns<T>(fields: FormField[]): ColumnSetting<T>[] {
  return fields.map(f => ({
    key: f.formControlName,
    header: f.label,
    isReference: f.isReference,
    valueGetter: (row: any) => {
      const raw = f.defaultValueGetter
        ? f.defaultValueGetter(row)
        : row[f.formControlName];

      if (f.isReference && typeof f.referenceLabel === 'function') {
        return f.referenceLabel(row);
      }

      if (f.isReference && typeof f.referenceLabel === 'string') {
        return row[f.referenceLabel] ?? '';
      }

      // pokud je to FormSelect objekt
      if (raw && typeof raw === 'object' && 'displayValue' in raw) {
        return (raw as { displayValue: string }).displayValue;
      }

      return raw != null ? raw : '';
    }
  }));
}

export function round(value: number, decimals: number) {
  const factor = Math.pow(10, decimals);
  return Math.round((value + Number.EPSILON) * factor) / factor;
}
