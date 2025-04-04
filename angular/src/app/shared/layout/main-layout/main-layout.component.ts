import {Component, OnInit} from '@angular/core';
import {MatDrawer, MatDrawerContainer} from '@angular/material/sidenav';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  standalone: false,
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent implements OnInit {
  ngOnInit(): void {
  }

}
