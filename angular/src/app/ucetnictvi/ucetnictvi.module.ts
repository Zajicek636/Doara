import {NgModule} from '@angular/core';
import {UcetniJednotkaRoutingModule} from './ucetnictvi-routing.module';
import {SeznamFakturComponent} from './seznam-faktur/seznam-faktur.component';
import {NovaFakturaComponent} from './nova-faktura/nova-faktura.component';
import {NovaPolozkaKontejnerModal} from './nova-faktura/nova-polozka-kontejner-modal';

@NgModule({
  imports: [
    UcetniJednotkaRoutingModule,
    SeznamFakturComponent,
    NovaFakturaComponent,
    NovaPolozkaKontejnerModal
  ],
  exports: []
})
export class UcetnictviModule { }

