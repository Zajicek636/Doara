import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {MatButton, MatButtonModule} from '@angular/material/button';
import {MatDrawer, MatDrawerContainer, MatSidenavModule} from '@angular/material/sidenav';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatList, MatListItem} from '@angular/material/list';
import {MainLayoutComponent} from "./main-layout/main-layout.component";
import {HeaderComponent} from "./header/header.component";
import {MenuComponent} from "./menu/menu.component";
import {MatIcon} from "@angular/material/icon";

@NgModule({
    declarations: [
        MainLayoutComponent,
        HeaderComponent,
        MenuComponent,
    ],
    imports: [
        CommonModule,
        RouterModule,
        MatTooltipModule,
        MatSidenavModule,
        MatButtonModule,
        MatListItem,
        MatList,
        MatSidenavModule,
        MatIcon,
    ]
})
export class LayoutModule { }
