import {
  Component,
  ContentChild,
  EventEmitter, HostListener,
  Input,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
  ViewContainerRef, ViewEncapsulation
} from '@angular/core';
import {FormControl, FormGroup, ValidationErrors} from '@angular/forms';
import {FormField, FormFieldTypes} from '../form.interfaces';
import {FormService} from '../form.service';
import {SharedModule} from '../../shared.module';
import {NgClass, NgForOf, NgSwitch, NgSwitchCase, NgSwitchDefault} from '@angular/common';
import {TextFieldComponent} from './text-field/text-field.component';
import {NumberFieldComponent} from './number-field/number-field.component';
import {LookupFieldComponent} from './lookup-field/lookup-field.component';

export interface FormComponentResult<T> {
  valid: boolean;
  data: T;
  modified: boolean;
  form: FormGroup;
}

@Component({
  selector: 'app-any-form',
  imports: [
    SharedModule,
    NgForOf,
    TextFieldComponent,
    NumberFieldComponent,
    NgSwitch,
    NgSwitchCase,
    LookupFieldComponent,
  ],
  templateUrl: './any-form.component.html',
  styleUrl: './any-form.component.scss',
})
export class AnyFormComponent<T> implements OnInit {
  @Input() fields!: FormField[];

  @Output() formChanged: EventEmitter<FormComponentResult<T>> = new EventEmitter<FormComponentResult<T>>();

  @Input() defaults: any = {}

  isSmallScreen: boolean = false;

  @HostListener('window:resize', ['$event'])
  onResize(event: any): void {
    this.isSmallScreen = event.target.innerWidth < 600;
  }

  public entity: any = {}
  modified: boolean = false;
  form!: FormGroup;
  formFieldTypes = FormFieldTypes;

  constructor(private formService: FormService) {}

  ngOnInit(): void {
    this.form = this.formService.createForm(this.fields);

    this.fields.forEach( f => {
      const control = this.form.get(f.formControlName)
      if(control) {
        control.markAsTouched()
        control.valueChanges.subscribe( val => {
          this.triggerOnChange(f.formControlName)
        })
      }
    })
    this.modified = false;
    //this.form.valueChanges.subscribe( x=> this.triggerOnChange(x));
  }

  triggerOnChange(control: any) {
    setTimeout(() => {
      const FormVal = Object.assign({}, this.entity, this.form.value);

      if(JSON.stringify(this.defaults[control]) !== JSON.stringify(FormVal[control])) {
        this.modified = true;
      }

      this.formChanged.emit({
        valid: this.form.valid,
        data: FormVal,
        modified: this.modified,
        form: this.form
      })
    })
  }
}
