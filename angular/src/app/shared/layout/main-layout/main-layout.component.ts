import {AfterViewInit, Component, OnInit, ViewChild, ViewContainerRef} from '@angular/core';
import {MatDrawer, MatDrawerContainer, MatSidenav} from '@angular/material/sidenav';
import {DrawerService} from '../drawer.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  standalone: false,
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent implements OnInit, AfterViewInit {

  constructor(
    private drawerService: DrawerService
  ) { }
  leftMenuCollapsed = true

  @ViewChild('leftDrawer') leftDrawer!: MatSidenav;

  @ViewChild('drawer') drawer!: MatSidenav;
  @ViewChild('drawerOutlet', { read: ViewContainerRef }) outlet!: ViewContainerRef;

  ngOnInit(): void {
  }

  onLeftDrawerClick(event: any) {
    this.leftDrawer.toggle();
    this.leftMenuCollapsed = !this.leftMenuCollapsed
  }

  ngAfterViewInit(): void {
    this.drawerService.setDrawer(this.drawer);
    this.drawerService.setContainerRef(this.outlet);
  }



}
