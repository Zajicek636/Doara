﻿import {Injectable} from '@angular/core';
import {NativeDateAdapter} from '@angular/material/core';

@Injectable()
export class CustomDateAdapter extends NativeDateAdapter {
  override parse(value: any): Date | null {
    if ((typeof value === 'string') && value.includes('.')) {
      const parts = value.split('.');
      if (parts.length === 3) {
        const day = +parts[0];
        const month = +parts[1] - 1;
        const year = +parts[2];
        return new Date(year, month, day);
      }
    }
    return super.parse(value);
  }

  override format(date: Date, displayFormat: any): string {
    const day = this._to2digit(date.getDate());
    const month = this._to2digit(date.getMonth() + 1);
    const year = date.getFullYear();
    return `${day}.${month}.${year}`;
  }

  private _to2digit(n: number) {
    return ('00' + n).slice(-2);
  }
}
