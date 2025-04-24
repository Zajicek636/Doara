import { Component } from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {ControlContainer, FormGroupDirective} from '@angular/forms';

@Component({
  selector: 'app-textarea-field',
  templateUrl: './textarea-field.component.html',
  styleUrls: ['./textarea-field.component.scss','../any-form.component.scss'],
  standalone: false,
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class TextareaFieldComponent extends BaseFieldComponent{

}
