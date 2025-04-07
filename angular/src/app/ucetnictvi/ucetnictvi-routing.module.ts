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
        data: { basePath: 'ucetnictvi' }
      },
     /* {
        path: 'edit/:id',
        component: '',
        data: { basePath: 'ucetnictvi',}
      },*/
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UcetniJednotkaRoutingModule { }
