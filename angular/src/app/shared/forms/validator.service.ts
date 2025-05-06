import {Injectable} from '@angular/core';
import {AbstractControl, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class ValidatorService {
  emailValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
      return emailPattern.test(control.value) ? null : {emailInvalid: true}
    }
  }

  patternValidator(pattern: string): ValidatorFn {
    const regex = new RegExp(pattern);
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (value == null || value === '') return null;

      return regex.test(value) ? null : { pattern: true };
    };
  }

  stripTime(date: Date): Date {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }

  minDateValidator(minDate: Date): ValidatorFn {
    const min = this.stripTime(minDate);
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (value == null || value === '') return null;

      const controlDate = this.stripTime(value instanceof Date ? value : new Date(value));
      if (isNaN(controlDate.getTime())) return { invalidDate: true };

      return controlDate >= min ? null : { minDate: { minDate: min } };
    };
  }

  maxDateValidator(maxDate: Date): ValidatorFn {
    const max = this.stripTime(maxDate);
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (value == null || value === '') return null;

      const controlDate = this.stripTime(value instanceof Date ? value : new Date(value));
      if (isNaN(controlDate.getTime())) return { invalidDate: true };

      return controlDate <= maxDate ? null : { maxDate: { maxDate: max } };
    };
  }

  decimalPlacesValidator(places: number): ValidatorFn {
    const re = new RegExp(`^\\d+(?:\\.\\d{0,${places}})?$`);
    return (control: AbstractControl): ValidationErrors | null => {
      const val = control.value?.toString();
      if (!val || re.test(val)) {
        return null;
      }
      return { decimalPlaces: { required: places } };
    };
  }

}
