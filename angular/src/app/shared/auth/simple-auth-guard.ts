import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
@Injectable({ providedIn: 'root' })
export class SimpleAuthGuard implements CanActivate {
  constructor(private oauthService: OAuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.oauthService.hasValidAccessToken()) {
      return true;
    }

    // Nepřihlášený → redirect
    this.oauthService.initLoginFlow();
    return false;
  }
}
