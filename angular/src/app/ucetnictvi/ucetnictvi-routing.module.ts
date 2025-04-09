import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UcetnictviMainComponent} from './ucetnictvi-main/ucetnictvi-main.component';


const routes: Routes = [
  {
    path: '',
    component: UcetnictviMainComponent,
    data: { basePath: 'ucetnictvi'},
    children: [
      {
        path: '',
        component: UcetnictviMainComponent,
        data: { basePath: 'ucetnictvi', breadcrumb: 'Účetnictví' }
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UcetniJednotkaRoutingModule { }
