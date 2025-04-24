import {Component} from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {CustomValidator, FormGroupedSelect, FormSelect} from '../../form.interfaces';


@Component({
  selector: 'app-lookup-field',
  standalone: false,
  templateUrl: './lookup-field.component.html',
  styleUrls: ['./lookup-field.component.scss','../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class LookupFieldComponent extends BaseFieldComponent {

  get canBeNulled(): boolean {
    if(this.field?.validator)
      return !this.field.validator.some(v => v.validator === CustomValidator.REQUIRED);
    else
      return true
  }

  get isGroupSelection(): boolean {
    return !!this.field?.options?.[0]?.hasOwnProperty('groupName');
  }

  get groupedOptions(): FormGroupedSelect[] | undefined {
    if (this.isGroupSelection) {
      return this.field.options as FormGroupedSelect[];
    }
    return undefined;
  }

  get normalOptions(): FormSelect[] | undefined {
    if (!this.isGroupSelection) {
      return this.field.options as FormSelect[];
    }
    return undefined;
  }

}
