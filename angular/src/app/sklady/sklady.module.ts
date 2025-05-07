import {ComponentType, NgModule} from '@angular/core';
import {SkladyRoutingModule} from './sklady-routing.module';
import {SkladyPolozkyComponent} from './sklady-polozky/sklady-polozky.component';
import {SkladyPolozkyDataService} from './sklady-polozky/data/sklady-polozky-data.service';
import {SkladyPohybyPolozkyComponent} from './sklady-editace/sklady-pohyby-polozky.component';


@NgModule({
  providers: [SkladyPolozkyDataService],
  declarations: [ ],
  imports: [
    SkladyRoutingModule,
    SkladyPolozkyComponent,
    SkladyPohybyPolozkyComponent,
  ],
  exports: [
  ]
})
export class SkladyModule { }

