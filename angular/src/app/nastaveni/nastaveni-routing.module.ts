import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {NastaveniMainComponent} from './nastaveni-main/nastaveni-main.component';

const routes: Routes = [
  {
    path: '',
    component: NastaveniMainComponent,
    data: { basePath: 'nastaveni'},
    children: [
      {
        path: '',
        component: NastaveniMainComponent,
        data: { basePath: 'nastaveni' }
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
export class NastaveniRoutingModule { }
