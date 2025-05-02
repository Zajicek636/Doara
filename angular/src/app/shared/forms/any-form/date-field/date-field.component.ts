import { Component } from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {ControlContainer, FormGroupDirective} from '@angular/forms';

@Component({
  selector: 'app-date-field',
  standalone: false,
  templateUrl: './date-field.component.html',
  styleUrls: ['./date-field.component.scss','../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class DateFieldComponent extends BaseFieldComponent {

}
