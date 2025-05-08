import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Router} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';
import {BaseMaterialIcons} from '../../../../styles/material.icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  standalone: false,
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  @Output() toggleLeft: EventEmitter<any> = new EventEmitter();
  constructor(private router: Router, private oauthService: OAuthService ) { }

  ngOnInit(): void {
  }

  onLeftToggle() {
    this.toggleLeft.emit(this.toggleLeft);
  }

  navigateHome() {
    this.router.navigate(['']);
  }

  logout() {
    this.oauthService.logOut()
  }

  protected readonly BaseMaterialIcons = BaseMaterialIcons;
}
