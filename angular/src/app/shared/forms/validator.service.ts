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
    return (control: AbstractControl): ValidationErrors | null => {
      const regex = new RegExp(pattern);
      return regex.test(control.value) ? null : { pattern: true }
    }
  }
}
