import {HttpClient} from '@angular/common/http';
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
    this.oauthService.events.subscribe(event => {
      switch (event.type) {
        case 'token_expires':
          console.warn('Token expired');
          this.oauthService.initLoginFlow();
          break;
        case 'token_received':
          console.log('New token recieved');
          break;
        case 'token_error':
          console.error('Token was not recieved');
          break;
      }
    });
  }

  public async auth() {
    await this.oauthService.loadDiscoveryDocument();
    const loginResult = await this.oauthService.tryLogin({
      onTokenReceived: () => {
        console.log('Redirecting to home');
        window.location.replace('/');
      }
    });

    // detekuj chybné uložení tokenů (případ kdy localStorage obsahuje bordel)
    const hasToken = this.oauthService.hasValidAccessToken();

    if (hasToken) {
      console.log('Login successful');
      this.oauthService.setupAutomaticSilentRefresh();
      const tenant = this.cookieService.get('__tenant');
      localStorage.setItem('tenant', tenant);
    } else {
      console.warn('No valid token, clearing storage and redirecting');

      // odložený redirect, aby měl promazání čas proběhnout
      setTimeout(() => {
        localStorage.clear();
        sessionStorage.clear();
        this.cookieService.deleteAll('/', '.localhost');
        document.cookie.split(";").forEach(c => {
          const eqPos = c.indexOf("=");
          const name = eqPos > -1 ? c.substring(0, eqPos) : c;
          document.cookie = `${name}=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/`;
        });

        this.oauthService.initLoginFlow();
      }, 0);
    }
  }
}
