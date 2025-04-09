import {FormBuilder, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import {Injectable} from '@angular/core';
import {CustomValidator, FormField, ValidatorConfig} from './form.interfaces';
import {ValidatorService} from './validator.service';

@Injectable({ providedIn: 'root' })
export class FormService {
  constructor(private fb: FormBuilder, private validatorService: ValidatorService) {}

  createForm(fields: FormField[]): FormGroup {
    const group: Record<string, any> = {};

    fields.forEach(field => {
      const control = this.fb.control(
        field.defaultValue ?? '',
        this.getValidators(field.validator)
      );
      group[field.formControlName] = control;
    });

    return this.fb.group(group);
  }

  private getValidators(validatorsConfig: ValidatorConfig[]): ValidatorFn[] {
    const validators: ValidatorFn[] = [];

    validatorsConfig.forEach(v => {
      switch (v.validator) {
        case CustomValidator.REQUIRED:
          validators.push(Validators.required);
          break;
        case CustomValidator.MIN:
          if (typeof v.params === 'number') {
            validators.push(Validators.min(v.params));
          }
          break;
        case CustomValidator.MAX:
          if (typeof v.params === 'number') {
            validators.push(Validators.max(v.params));
          }
          break;
        case CustomValidator.EMAIL:
          validators.push(this.validatorService.emailValidator());
          break;
        case CustomValidator.PATTERN:
          if (typeof v.params === 'string') {
            validators.push(this.validatorService.patternValidator(v.params));
          }
          break;
      }
    });

    return validators;
  }
}
