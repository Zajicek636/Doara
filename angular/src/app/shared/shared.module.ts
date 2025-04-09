import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {LayoutModule} from "./layout/layout.module";
import {MatDialogModule} from "@angular/material/dialog";

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        LayoutModule,
        MatDialogModule
    ],
    exports: [
        ReactiveFormsModule,
        LayoutModule
    ],
    providers: [],
})
export class SharedModule { }

