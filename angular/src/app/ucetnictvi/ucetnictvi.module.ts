import {NgModule} from '@angular/core';
import {UcetniJednotkaRoutingModule} from './ucetnictvi-routing.module';
import {SeznamFakturComponent} from './seznam-faktur/seznam-faktur.component';
import {NovaFakturaComponent} from './nova-faktura/nova-faktura.component';

@NgModule({
  imports: [
    UcetniJednotkaRoutingModule,
    SeznamFakturComponent,
    NovaFakturaComponent
  ],
  exports: []
})
export class UcetnictviModule { }

