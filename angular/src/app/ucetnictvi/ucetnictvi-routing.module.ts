﻿import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UcetnictviMainComponent} from './ucetnictvi-main/ucetnictvi-main.component';
import {SkladyPolozkyComponent} from '../sklady/sklady-polozky/sklady-polozky.component';
import {SeznamFakturComponent} from './seznam-faktur/seznam-faktur.component';
import {NovaFakturaComponent} from './nova-faktura/nova-faktura.component';
import {SubjektyComponent} from './subjekty/subjekty.component';


const routes: Routes = [
  {
    path: '',
    component: UcetnictviMainComponent,
    data: { basePath: 'ucetnictvi'},
    children: [
      {
        path: '',
        redirectTo: 'seznam-dokladu',
        pathMatch: 'full'
      },
      {
        path: 'seznam-dokladu',
        component: SeznamFakturComponent,
        data: { basePath: 'ucetnictvi', breadcrumb: 'Seznam dokladů' },
      },
      {
        path: 'doklad',
        children: [
          { path: '', component: NovaFakturaComponent, data: { breadcrumb: 'Nová faktura' } },
          { path: ':id', component: NovaFakturaComponent, data: { breadcrumb: 'Úprava faktury' } }
        ]
      },
      {
        path: 'subjekty',
        component: SubjektyComponent,
        data: { basePath: 'ucetnictvi', breadcrumb: 'Seznam subjektů' },
        children: []
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UcetniJednotkaRoutingModule { }
