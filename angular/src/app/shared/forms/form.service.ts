import {FormBuilder, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import {Injectable} from '@angular/core';
import {CustomValidator, FormField, FormFieldTypes} from './form.interfaces';
import {ValidatorService} from './validator.service';

@Injectable({ providedIn: 'root' })
export class FormService {
  constructor(private fb: FormBuilder, private validatorService: ValidatorService) {}

  createForm(fields: FormField[]): FormGroup {
    const group: Record<string, any> = {};

    fields.forEach(field => {
      const control = this.fb.control(
        field.defaultValue ?? '',
        this.getValidators(field)
      );
      group[field.formControlName] = control;
    });
    return this.fb.group(group);
  }

  private getValidators(field: FormField): ValidatorFn[] {
    const validators: ValidatorFn[] = [];
    if(!field.validator) return [];

    field.validator.forEach(v => {
      switch (v.validator) {
        case CustomValidator.REQUIRED:
          validators.push(Validators.required);
          break;
        case CustomValidator.MIN:
          if (!v.params || typeof v.params !== 'number') break;

          if (field.type === FormFieldTypes.TEXT) {
            validators.push(Validators.minLength(v.params));
          } else if (field.type === FormFieldTypes.NUMBER) {
            validators.push(Validators.min(v.params));
          }
          break;
        case CustomValidator.MAX:
          if (!v.params || typeof v.params !== 'number') break;

          if (field.type === FormFieldTypes.TEXT) {
            validators.push(Validators.maxLength(v.params));
          } else if (field.type === FormFieldTypes.NUMBER) {
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
