import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {AuthService} from './shared/auth/auth.service';
import {OAuthService} from 'angular-oauth2-oidc';
import {CookieService} from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: false,
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService) {
  }

  async ngOnInit() {
    await this.authService.auth()
  }
}
