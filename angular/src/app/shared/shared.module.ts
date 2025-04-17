import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {LayoutModule} from "./layout/layout.module";
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {DynamicTableComponent} from './table/table/table.component';

@NgModule({
    declarations: [
      DynamicTableComponent
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
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  exports: [
    ReactiveFormsModule,
    LayoutModule,
    DynamicTableComponent,
  ],
    providers: [
    ],
})
export class SharedModule { }

