import {Component} from '@angular/core';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {BaseFieldComponent} from '../base-field.component';
import {CustomValidator, ValidatorConfig} from '../../form.interfaces';
import {round} from '../../form-field.utils';

@Component({
  selector: 'app-number-field',
  standalone:false,
  templateUrl: './number-field.component.html',
  styleUrls: ['./number-field.component.scss','../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class NumberFieldComponent extends BaseFieldComponent{

  onBlur() {
    const ctrl = this.control;
    if (!ctrl) return;

    const raw = parseFloat(ctrl.value);
    if (isNaN(raw)) return;

    const decimalConfig = (this.field.validator as ValidatorConfig[] ?? [])
      .find(v => v.validator === CustomValidator.DECIMAL_PLACES && typeof v.params === 'number');
    if (!decimalConfig) {
      return;
    }

    const decimals = decimalConfig.params as number;
    const rounded = round(raw, decimals);
    ctrl.setValue(rounded, { emitEvent: false });
  }
}
