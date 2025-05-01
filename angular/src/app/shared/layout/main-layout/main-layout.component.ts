import {Component, OnInit, ViewChild} from '@angular/core';
import {MatDrawer, MatDrawerContainer, MatSidenav} from '@angular/material/sidenav';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  standalone: false,
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent implements OnInit {
  leftMenuCollapsed = true

  @ViewChild('leftDrawer') leftDrawer!: MatSidenav;

  ngOnInit(): void {
  }

  onLeftDrawerClick(event: any) {
    this.leftDrawer.toggle();
    this.leftMenuCollapsed = !this.leftMenuCollapsed
  }


}
