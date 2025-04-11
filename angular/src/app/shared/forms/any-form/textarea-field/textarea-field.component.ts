import { Component } from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {ControlContainer, FormGroupDirective} from '@angular/forms';

@Component({
  selector: 'app-textarea-field',
  templateUrl: './textarea-field.component.html',
  styleUrl: './textarea-field.component.scss',
  standalone: false,
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class TextareaFieldComponent extends BaseFieldComponent{

}
