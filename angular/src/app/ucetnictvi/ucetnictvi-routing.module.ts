import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UcetnictviMainComponent} from './ucetnictvi-main/ucetnictvi-main.component';
import {SkladyPolozkyComponent} from '../sklady/sklady-polozky/sklady-polozky.component';
import {SeznamFakturComponent} from './seznam-faktur/seznam-faktur.component';
import {NovaFakturaComponent} from './nova-faktura/nova-faktura.component';


const routes: Routes = [
  {
    path: '',
    component: UcetnictviMainComponent,
    data: { basePath: 'ucetnictvi'},
    children: [
      {
        path: '',
        redirectTo: 'seznam-faktur',
        pathMatch: 'full'
      },
      {
        path: 'seznam-faktur',
        component: SeznamFakturComponent,
        data: { basePath: 'ucetnictvi', breadcrumb: 'Seznam faktur' },
        children: [
          /*   {
      path: 'nastaveni',
      component: SkladyEditaceComponent,
      data: { basePath: 'sklady', breadcrumb: 'Nastaveni'}
    },*/
        ],
      },
      {
        path: 'nova-faktura',
        component: NovaFakturaComponent,
        data: { basePath: 'ucetnictvi', breadcrumb: 'Nová faktura' },
        children: [
          /*   {
      path: 'nastaveni',
      component: SkladyEditaceComponent,
      data: { basePath: 'sklady', breadcrumb: 'Nastaveni'}
    },*/
        ],
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UcetniJednotkaRoutingModule { }
