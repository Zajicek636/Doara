import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routes';
import {LayoutModule} from '@angular/cdk/layout';
import {SharedModule} from './shared/shared.module';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {loadingInterceptor} from './shared/loading/loading.interceptor';
import {requestVerificationInterceptor} from './shared/auth/auth.interceptor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule
  ],
  providers: [
    provideHttpClient( withInterceptors([loadingInterceptor, requestVerificationInterceptor])),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
