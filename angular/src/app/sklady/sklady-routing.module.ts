import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {SkladyMainComponent} from './sklady-main/sklady-main.component';


const routes: Routes = [
  {
    path: '',
    component: SkladyMainComponent,
    data: { basePath: 'sklady'},
    children: [
      {
        path: '',
        component: SkladyMainComponent,
        data: { basePath: 'sklady' }
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
export class SkladyRoutingModule { }
