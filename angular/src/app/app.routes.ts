import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainLayoutComponent} from './shared/layout/main-layout/main-layout.component';
import {HomeComponent} from './shared/home/home.component';
import {SkladyMainComponent} from './sklady/sklady-main/sklady-main.component';
import {UcetnictviMainComponent} from './ucetnictvi/ucetnictvi-main/ucetnictvi-main.component';

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
        path: 'home', //tady nekde jde hodit auth, jestli mas pristup k modulu :-)))
        component: HomeComponent,
      },
      {
        path: 'ucetnictvi',
        loadChildren: ucetnictviModule
      },
      {
        path: 'sklady',
        loadChildren: skladyModule
      },
      {
        path: 'nastaveni',
        loadChildren: nastaveniModule
      }
    ]
  }
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
