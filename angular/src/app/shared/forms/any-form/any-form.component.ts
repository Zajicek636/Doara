import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup, ValidationErrors} from '@angular/forms';
import {FormField, FormFieldTypes} from '../form.interfaces';
import {FormService} from '../form.service';
import {SharedModule} from '../../shared.module';
import {NgForOf} from '@angular/common';
import {TextFieldComponent} from './text-field/text-field.component';
import {NumberFieldComponent} from './number-field/number-field.component';

@Component({
  selector: 'app-any-form',
  imports: [
    SharedModule,
    NgForOf,
    TextFieldComponent,
    NumberFieldComponent
  ],
  templateUrl: './any-form.component.html',
  styleUrl: './any-form.component.scss'
})
export class AnyFormComponent implements OnInit {
  @Input() fields!: FormField[];

  form!: FormGroup;
  formFieldTypes = FormFieldTypes;

  constructor(private formService: FormService) {}

  ngOnInit(): void {
    const res = this.formService.createForm(this.fields);
    this.form = res
  }

 /* getErrorMessage(name: string): string {
    const control = this.form.get(name);
    if(control) {
      const controlErrors = control.errors;
      if(controlErrors) {
        let error: string[] = []
        Object.keys(controlErrors).forEach(x => {
          error.push(this.parseErrorMessage(x, controlErrors));
        })
        return error.join('. ');
      }
    }
    return ''
  }

  parseErrorMessage(key: string, errors: ValidationErrors): string {
    if (key === 'required' && errors['required']) {
      return 'Toto pole je povinné';
    }
    if (key === 'emailInvalid' && errors['emailInvalid']) {
      return 'Neplatná emailová adresa';
    }
    if (key === 'pattern' && errors['pattern']) {
      return 'Neplatný formát';
    }
    if (key === 'min' && errors['min']) {
      return `Hodnota je příliš nízká, minimálně ${errors['min'].min}`;
    }
    if (key === 'max' && errors['max']) {
      return `Hodnota je příliš vysoká, maximálně ${errors['max'].max}`;
    }
    return ''
  }*/
}
