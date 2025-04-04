import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {LayoutModule} from "./layout/layout.module";

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        LayoutModule
    ],
    exports: [
        ReactiveFormsModule,
        LayoutModule
    ]
})
export class SharedModule { }

