import { NgModule } from '@angular/core';
import {AsyncPipe, CommonModule, NgClass, NgForOf, NgIf} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {LayoutModule} from "./layout/layout.module";
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIcon, MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {DynamicTableComponent} from './table/table/table.component';
import {AnyFormModule} from './forms/any-form/any-form.module';
import {ContextToolbarComponent} from './context-toolbar/context-toolbar.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatDividerModule} from '@angular/material/divider';
import {MatCardModule,} from '@angular/material/card';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {ToolbarButtonComponent} from './context-toolbar/toolbar-button/toolbar-button.component';
import {MatTooltip} from '@angular/material/tooltip';


@NgModule({
    declarations: [
      DynamicTableComponent,
      ContextToolbarComponent,
      ToolbarButtonComponent,
    ],
    imports: [
        MatProgressSpinnerModule,
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
        MatToolbarModule,
        MatDividerModule,
        AsyncPipe,
        NgClass,
        MatTooltip
    ],
  exports: [
    NgIf,
    ReactiveFormsModule,
    LayoutModule,
    DynamicTableComponent,
    AnyFormModule,
    ContextToolbarComponent,
    ToolbarButtonComponent,
    MatDividerModule,
    NgForOf,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    NgClass,
  ],
    providers: [
    ],
})
export class SharedModule { }

