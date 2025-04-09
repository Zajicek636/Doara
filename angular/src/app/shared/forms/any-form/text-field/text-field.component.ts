import {Component, Input} from '@angular/core';
import {ControlContainer, FormGroup, FormGroupDirective} from '@angular/forms';
import {FormField, FormFieldTypes} from '../../form.interfaces';
import {SharedModule} from '../../../shared.module';
import {MatFormField, MatInput, MatError, MatLabel} from '@angular/material/input';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-text-field',
  imports: [
    SharedModule,
    MatFormField,
    MatInput,
    MatError,
    MatLabel,
    NgIf,
  ],
  templateUrl: './text-field.component.html',
  styleUrl: './text-field.component.scss',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class TextFieldComponent {
  @Input() field!: FormField;
  @Input() form!: FormGroup;
  get errorMessage(): string {
    const control = this.form.get(this.field.formControlName);
    if (control && control.errors) {
      if (control.errors['required']) {
        return 'Toto pole je povinné';
      }
    }
    return '';
  }

  protected readonly FormFieldTypes = FormFieldTypes;
}
