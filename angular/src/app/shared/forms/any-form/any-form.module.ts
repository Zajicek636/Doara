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

@NgModule({
  declarations: [
    AnyFormComponent,
    TextareaFieldComponent,
    LookupFieldComponent,
    TextFieldComponent,
    NumberFieldComponent,
    AutocompleteFieldComponent
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
    MatAutocompleteModule
  ],
  exports: [
    ReactiveFormsModule,
    LayoutModule,
    AnyFormComponent,
    TextareaFieldComponent,
    LookupFieldComponent,
    TextFieldComponent,
    NumberFieldComponent,
  ],
  providers: [],
})
export class AnyFormModule { }

