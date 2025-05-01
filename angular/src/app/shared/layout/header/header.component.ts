import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  standalone: false,
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  @Output() toggleLeft: EventEmitter<any> = new EventEmitter();
  constructor(private router: Router,) { }

  ngOnInit(): void {
  }

  onLeftToggle() {
    this.toggleLeft.emit(this.toggleLeft);
  }

  navigateHome() {
    this.router.navigate(['']);
  }

  logout() {
    this.router.navigate(['/home']);
  }

}
