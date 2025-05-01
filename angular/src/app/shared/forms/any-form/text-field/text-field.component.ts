import {Component, signal} from '@angular/core';
import {ControlContainer, FormGroupDirective} from '@angular/forms';
import {BaseFieldComponent} from '../base-field.component';

@Component({
  selector: 'app-text-field',
  standalone: false,
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
}
