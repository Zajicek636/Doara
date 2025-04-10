import {Component} from '@angular/core';
import { FormFieldTypes} from '../../form.interfaces';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {MatError, MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {NgIf} from '@angular/common';
import {SharedModule} from '../../../shared.module';
import {BaseFieldComponent} from '../base-field.component';

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
  styleUrl: './number-field.component.scss',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class NumberFieldComponent extends BaseFieldComponent{

  protected readonly FormFieldTypes = FormFieldTypes;
}
