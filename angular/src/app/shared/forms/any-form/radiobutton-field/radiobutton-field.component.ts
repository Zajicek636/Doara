import { Component } from '@angular/core';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {BaseFieldComponent} from '../base-field.component';
import {FormSelect} from '../../form.interfaces';

@Component({
  selector: 'app-radiobutton-field',
  standalone: false,
  templateUrl: './radiobutton-field.component.html',
  styleUrls: ['./radiobutton-field.component.scss','../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class RadiobuttonFieldComponent extends BaseFieldComponent{
  get options(): FormSelect[] {
    return (this.field.options || []) as FormSelect[];
  }
}
