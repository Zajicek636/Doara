import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { SkladyComponent } from './components/sklady.component';
import { SkladyRoutingModule } from './sklady-routing.module';

@NgModule({
  declarations: [SkladyComponent],
  imports: [CoreModule, ThemeSharedModule, SkladyRoutingModule],
  exports: [SkladyComponent],
})
export class SkladyModule {
  static forChild(): ModuleWithProviders<SkladyModule> {
    return {
      ngModule: SkladyModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<SkladyModule> {
    return new LazyModuleFactory(SkladyModule.forChild());
  }
}
