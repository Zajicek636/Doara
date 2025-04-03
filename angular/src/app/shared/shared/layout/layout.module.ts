import {NgModule} from '@angular/core';
import {MainLayoutComponent} from './main-layout/main-layout/main-layout.component';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {HeaderComponent} from './header/header/header.component';
import {MatButton, MatButtonModule} from '@angular/material/button';
import {MatDrawer, MatDrawerContainer, MatSidenavModule} from '@angular/material/sidenav';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MenuComponent} from './menu/menu/menu.component';
import {MatList, MatListItem} from '@angular/material/list';

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
  ]
})
export class LayoutModule { }
