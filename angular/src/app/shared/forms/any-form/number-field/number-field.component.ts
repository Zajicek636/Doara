import {Component} from '@angular/core';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {BaseFieldComponent} from '../base-field.component';

@Component({
  selector: 'app-number-field',
  standalone:false,
  templateUrl: './number-field.component.html',
  styleUrls: ['./number-field.component.scss','../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class NumberFieldComponent extends BaseFieldComponent{

}
