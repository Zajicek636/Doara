import { Component } from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {MatError, MatFormField, MatHint, MatInput, MatLabel, MatSuffix} from '@angular/material/input';
import {MatSelect} from '@angular/material/select';
import {MatOption} from '@angular/material/core';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {NgFor, NgIf} from '@angular/common';
import {FormFieldTypes} from '../../form.interfaces';
import {SharedModule} from '../../../shared.module';

@Component({
  selector: 'app-lookup-field',
  standalone: true,
  imports: [
    MatFormField,
    MatLabel,
    MatSelect,
    MatOption,
    MatError,
    NgIf,
    NgFor,
    SharedModule
  ],
  templateUrl: './lookup-field.component.html',
  styleUrl: './lookup-field.component.scss',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class LookupFieldComponent extends BaseFieldComponent {
  protected readonly FormFieldTypes = FormFieldTypes;
}
