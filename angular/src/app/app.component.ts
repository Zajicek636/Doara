import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {AuthService} from './shared/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: false,
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService) {
    this.authService.autoLogin();
  }

  async ngOnInit() {
    //todo pro auth subscription
  }
}
