import {NgModule} from '@angular/core';
import {SkladyRoutingModule} from './sklady-routing.module';
import {SkladyPolozkyComponent} from './sklady-polozky/sklady-polozky.component';
import {SkladyEditaceComponent} from './sklady-editace/sklady-editace.component';
import {HttpClient} from '@angular/common/http';
import {SkladyPolozkyDataService} from './sklady-polozky/sklady-polozky-data.service';

@NgModule({
  providers: [SkladyPolozkyDataService],
  declarations: [],
  imports: [
    SkladyRoutingModule,
    SkladyPolozkyComponent,
    SkladyEditaceComponent,
  ],
  exports: [
  ]
})
export class SkladyModule { }

