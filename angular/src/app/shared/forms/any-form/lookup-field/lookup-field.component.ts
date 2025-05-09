import {Component} from '@angular/core';
import {BaseFieldComponent} from '../base-field.component';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {CustomValidator, FormGroupedSelect, FormSelect} from '../../form.interfaces';


@Component({
  selector: 'app-lookup-field',
  standalone: false,
  templateUrl: './lookup-field.component.html',
  styleUrls: ['./lookup-field.component.scss', '../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class LookupFieldComponent extends BaseFieldComponent {
  get canBeNulled(): boolean {
    return !this.field?.validator?.some(v => v.validator === CustomValidator.REQUIRED);
  }

  get isGroupSelection(): boolean {
    return !!this.field?.options?.[0]?.hasOwnProperty('groupName');
  }

  get groupedOptions(): FormGroupedSelect[] {
    return (this.field.options ?? []) as FormGroupedSelect[];
  }

  get normalOptions(): FormSelect[] {
    return (this.field.options ?? []) as FormSelect[];
  }

  compareWith = (o1: FormSelect, o2: FormSelect): boolean => {
    if (!o1 || !o2) return o1 === o2;
    return o1.value === o2.value;
  };
}
