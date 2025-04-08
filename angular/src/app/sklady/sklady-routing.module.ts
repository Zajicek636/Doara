import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {SkladyMainComponent} from './sklady-main/sklady-main.component';
import {SkladyPolozkyComponent} from './sklady-polozky/sklady-polozky.component';
import {SkladyEditaceComponent} from './sklady-editace/sklady-editace.component';
import {SkladyPolozkyDataService} from './sklady-polozky/sklady-polozky-data.service';


const routes: Routes = [
  {
    path: '',
    component: SkladyMainComponent, //necham to pro layout a pripadne breadcrumb, menu, zahlavi atd.
    data: { basePath: 'sklady'},
    children: [
      {
        path: '',
        redirectTo: 'polozky',
        pathMatch: 'full'
      },
      {
        path: 'polozky',
        component: SkladyPolozkyComponent,
        data: { basePath: 'sklady', dataService: SkladyPolozkyDataService}
      },
      {
        path: 'editace-skladu',
        component: SkladyEditaceComponent,
        data: { basePath: 'sklady',}
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SkladyRoutingModule { }
