import {Component, Input} from '@angular/core';
import {FormField, FormFieldTypes} from '../../form.interfaces';
import {FormGroup} from '@angular/forms';
import {MatError, MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {NgIf} from '@angular/common';
import {SharedModule} from '../../../shared.module';

@Component({
  selector: 'app-number-field',
  imports: [
    MatError,
    MatFormField,
    MatInput,
    MatLabel,
    NgIf,
    SharedModule,
    MatError
  ],
  templateUrl: './number-field.component.html',
  styleUrl: './number-field.component.scss'
})
export class NumberFieldComponent {
  @Input() field!: FormField;
  @Input() form!: FormGroup;
  get errorMessage(): string {
    const control = this.form.get(this.field.formControlName);
    if (control && control.errors) {
      if (control.errors['required']) {
        return 'Toto pole je povinné';
      }
      if (control.errors['min']) {
        return 'Zvýšit';
      }
      if (control.errors['max']) {
        return 'Zmenšit';
      }
    }
    return '';
  }

  protected readonly FormFieldTypes = FormFieldTypes;
}
