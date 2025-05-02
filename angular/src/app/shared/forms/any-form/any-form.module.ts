import {NgModule} from '@angular/core';
import {CommonModule, NgIf} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {LayoutModule} from '../../layout/layout.module';
import {MatDialogModule} from '@angular/material/dialog';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {TextareaFieldComponent} from './textarea-field/textarea-field.component';
import {AnyFormComponent} from './any-form.component';
import {LookupFieldComponent} from './lookup-field/lookup-field.component';
import {TextFieldComponent} from './text-field/text-field.component';
import {NumberFieldComponent} from './number-field/number-field.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {AutocompleteFieldComponent} from './autocomplete-field/autocomplete-field.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {RadiobuttonFieldComponent} from './radiobutton-field/radiobutton-field.component';
import {MatRadioModule} from '@angular/material/radio';
import {DateFieldComponent} from './date-field/date-field.component';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
  MatDateFormats,
  MatNativeDateModule,
} from '@angular/material/core';
import {CustomDateAdapter} from './date-field/date.adapter';

export const MY_DATE_FORMATS: MatDateFormats = {
  parse: {
    dateInput: 'dd.MM.yyyy',
  },
  display: {
    dateInput: 'dd.MM.yyyy',
    monthYearLabel: 'MMMM yyyy',
    dateA11yLabel: 'dd.MM.yyyy',
    monthYearA11yLabel: 'MMMM yyyy',
  },
};

@NgModule({
  declarations: [
    AnyFormComponent,
    TextareaFieldComponent,
    LookupFieldComponent,
    TextFieldComponent,
    NumberFieldComponent,
    AutocompleteFieldComponent,
    RadiobuttonFieldComponent,
    DateFieldComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    LayoutModule,
    MatDialogModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatRadioModule,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    MatNativeDateModule,
  ],
  exports: [
    ReactiveFormsModule,
    LayoutModule,
    AnyFormComponent,
    TextareaFieldComponent,
    LookupFieldComponent,
    TextFieldComponent,
    NumberFieldComponent,
    MatNativeDateModule,
    DateFieldComponent
  ],
  providers: [
    { provide: DateAdapter, useClass: CustomDateAdapter },
    { provide: MAT_DATE_LOCALE, useValue: 'cs-CZ' },
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }
  ]
})
export class AnyFormModule { }

