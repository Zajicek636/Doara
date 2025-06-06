﻿import {Directive, Input} from '@angular/core';
import {FormField, FormFieldTypes} from '../form.interfaces';
import {FormGroup, ValidationErrors} from '@angular/forms';

@Directive()
export abstract class BaseFieldComponent {
  @Input() field!: FormField;
  @Input() form!: FormGroup;


  protected readonly FormFieldTypes = FormFieldTypes;

  protected get control() {
    return this.form.get(this.field.formControlName);
  }

  protected parseErrorMessages(errors: ValidationErrors): string {
    const errorMessages: { [key: string]: string | ((error: any) => string) } = {
      required: 'Toto pole je povinné',
      min: (error: any) => `Minimum je ${error.min}`,
      minlength: (error: any) => `Nenaplněno minimum znaků: ${error.actualLength} / ${error.requiredLength}`,
      max: (error: any) => `Maximum je ${error.max}`,
      maxlength: (error: any) => `Překročeno maximum znaků: ${error.actualLength} / ${error.requiredLength}`,
      email: 'Email není ve správném formátu',
      pattern: (error: any) => `Položka není ve správném formátu`,
      minDate: (error: any) => `Datum nesmí být před ${new Date(error.minDate).toLocaleDateString()}`,
      maxDate: (error: any) => `Datum nesmí být po ${new Date(error.maxDate).toLocaleDateString()}`
    };

    const messages = Object.keys(errors).map(key => {
      const msgOrFn = errorMessages[key];
      if (typeof msgOrFn === 'function') {
        return msgOrFn(errors[key]);
      } else {
        return msgOrFn;
      }
    });
    return messages.join('. ');
  }

  protected get errorMessage(): string {
    const control = this.control;
    if (control && control.errors) {
      return this.parseErrorMessages(control.errors);
    }
    return '';
  }
}
