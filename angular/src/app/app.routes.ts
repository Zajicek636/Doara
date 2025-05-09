import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainLayoutComponent} from './shared/layout/main-layout/main-layout.component';
import {HomeComponent} from './shared/home/home.component';
import {SimpleAuthGuard} from './shared/auth/simple-auth-guard';

const ucetnictviModule = () => import('./ucetnictvi/ucetnictvi.module').then(m => m.UcetnictviModule);
const skladyModule = () => import('./sklady/sklady.module').then(m => m.SkladyModule);
const nastaveniModule = () => import('./nastaveni/nastaveni.module').then(m => m.NastaveniModule);
export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        component: HomeComponent,
        canActivate: [SimpleAuthGuard],
        data: { breadcrumb: "Home" }

      },
      {
        path: 'ucetnictvi',
        loadChildren: ucetnictviModule,
        canActivate: [SimpleAuthGuard],
      },
      {
        path: 'sklady',
        loadChildren: skladyModule,
        canActivate: [SimpleAuthGuard],
      },
    ]
  }
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
