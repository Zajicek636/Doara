import {Component, signal} from '@angular/core';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {FormFieldTypes} from '../../form.interfaces';
import {SharedModule} from '../../../shared.module';
import {MatFormField, MatInput, MatError, MatLabel, MatPrefix, MatSuffix} from '@angular/material/input';
import {NgIf} from '@angular/common';
import {BaseFieldComponent} from '../base-field.component';
import {MatIcon} from '@angular/material/icon';
import {MatIconButton} from '@angular/material/button';

@Component({
  selector: 'app-text-field',
  imports: [
    SharedModule,
    MatFormField,
    MatInput,
    MatError,
    MatLabel,
    NgIf,
    MatIcon,
    MatIconButton,
    MatSuffix,
  ],
  templateUrl: './text-field.component.html',
  styleUrl: './text-field.component.scss',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class TextFieldComponent extends BaseFieldComponent {
  hide = signal(true);
  hideShowPass(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  protected readonly FormFieldTypes = FormFieldTypes;
}
