import {HttpClient, HttpHeaders} from '@angular/common/http';
import {lastValueFrom, Observable, tap} from 'rxjs';
import {Injectable} from '@angular/core';
import {AuthConfig, OAuthService} from 'angular-oauth2-oidc';
import {CookieService} from 'ngx-cookie-service';


export const authConfig: AuthConfig = {
  issuer: 'https://localhost:44346/',
  redirectUri: window.location.origin,
  clientId: 'Doara_App',
  responseType: 'code',
  scope: 'Doara',
  oidc: true,
  useSilentRefresh: false,
  silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html',
  sessionChecksEnabled: false,
};

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient, private oauthService: OAuthService,private cookieService: CookieService) {
  }

  public async auth() {
    await this.oauthService.loadDiscoveryDocumentAndTryLogin();

    if (this.oauthService.hasValidAccessToken()) {
      this.oauthService.setupAutomaticSilentRefresh();
      const tenant = this.cookieService.get('__tenant');
      localStorage.setItem('tenant', tenant);
    } else {
      this.oauthService.initLoginFlow();
    }
  }
}
