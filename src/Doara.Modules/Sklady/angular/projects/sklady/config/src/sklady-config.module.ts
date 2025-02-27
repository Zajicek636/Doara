import { ModuleWithProviders, NgModule } from '@angular/core';
import { SKLADY_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class SkladyConfigModule {
  static forRoot(): ModuleWithProviders<SkladyConfigModule> {
    return {
      ngModule: SkladyConfigModule,
      providers: [SKLADY_ROUTE_PROVIDERS],
    };
  }
}
