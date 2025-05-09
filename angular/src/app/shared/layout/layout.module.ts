import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {MatButton, MatButtonModule} from '@angular/material/button';
import {MatDrawer, MatDrawerContainer, MatSidenavModule} from '@angular/material/sidenav';
import {MatTooltipModule} from '@angular/material/tooltip';
import {
  MatDivider,
  MatList,
  MatListItem,
  MatListItemIcon,
  MatListItemMeta,
  MatListItemTitle,
  MatNavList
} from '@angular/material/list';
import {MainLayoutComponent} from "./main-layout/main-layout.component";
import {HeaderComponent} from "./header/header.component";
import {MenuComponent} from "./menu/menu.component";
import {MatIcon} from "@angular/material/icon";
import {MenuItemComponent} from './menu/menu-item/menu-item.component';
import {MatToolbar} from '@angular/material/toolbar';
import {BreadcrumbComponent} from "../breadcrumb/breadcrumb.component";
import {LoadingComponent} from '../loading/loading.component';
import {MatProgressSpinner} from '@angular/material/progress-spinner';

@NgModule({
    declarations: [
      MainLayoutComponent,
      HeaderComponent,
      MenuComponent,
      MenuItemComponent,
      LoadingComponent
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
    MatToolbar,
    MatNavList,
    MatListItemIcon,
    MatListItemTitle,
    MatDivider,
    MatListItemMeta,
    BreadcrumbComponent,
    MatProgressSpinner,
  ]
})
export class LayoutModule { }
