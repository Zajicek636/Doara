﻿import {APP_INITIALIZER, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routes';
import {LayoutModule} from '@angular/cdk/layout';
import {SharedModule} from './shared/shared.module';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {loadingInterceptor} from './shared/loading/loading.interceptor';
import {requestVerificationInterceptor} from './shared/auth/auth.interceptor';
import {OAuthModule, OAuthService} from 'angular-oauth2-oidc';
import {authConfig} from './shared/auth/auth.service';
import {CookieService} from 'ngx-cookie-service';
import {AuthInitializerService} from './shared/auth/auth.initializer';


@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule,
    OAuthModule.forRoot()
  ],
  providers: [
    CookieService,
    provideHttpClient( withInterceptors([loadingInterceptor, requestVerificationInterceptor])),
    {
      provide: APP_INITIALIZER,
      useFactory: (authInit: AuthInitializerService) => () => authInit.init(),
      deps: [AuthInitializerService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(authConfig);
  }
}
