import {Component} from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {CustomValidator} from '../../form.interfaces';


@Component({
  selector: 'app-lookup-field',
  standalone: false,
  templateUrl: './lookup-field.component.html',
  styleUrl: './lookup-field.component.scss',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class LookupFieldComponent extends BaseFieldComponent {

  get canBeNulled(): boolean {
    if(this.field?.validator)
      return !this.field.validator.some(v => v.validator === CustomValidator.REQUIRED);
    else
      return true
  }
}
