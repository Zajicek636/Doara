import {
  Component,
  EventEmitter, HostListener,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {FormField, FormFieldTypes} from '../form.interfaces';
import {FormService} from '../form.service';

export interface FormComponentResult {
  valid: boolean;
  data: any;
  modified: boolean;
  form: FormGroup
}

@Component({
  selector: 'app-any-form',
  standalone: false,
  templateUrl: './any-form.component.html',
  styleUrl: './any-form.component.scss',
})
export class AnyFormComponent<T> implements OnInit {
  @Input() defaults: any = {}
  @Input() fields!: FormField[];

  @Output() formChanged: EventEmitter<FormComponentResult> = new EventEmitter<FormComponentResult>();
  @Output() formReady: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

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
    this.triggerInitialChange();
    this.formReady.emit(this.form)
  }

  private triggerInitialChange() {
    const FormVal = Object.assign({}, this.entity, this.form.value);

    this.formChanged.emit({
      valid: this.form.valid,
      data: FormVal,
      modified: false,
      form: this.form
    });
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
