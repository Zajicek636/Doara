import {Injectable, TemplateRef} from '@angular/core';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatSidenav } from '@angular/material/sidenav';
import { ViewContainerRef } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DrawerService {
  private drawer!: MatSidenav;
  private containerRef!: ViewContainerRef;

  setDrawer(drawer: MatSidenav) {
    this.drawer = drawer;
  }

  setContainerRef(ref: ViewContainerRef) {
    this.containerRef = ref;
  }

  openWithTemplate(template: TemplateRef<any>) {
    this.containerRef.clear();
    this.containerRef.createEmbeddedView(template);
    this.drawer.open();
  }

  open() {
    this.drawer?.open();
  }

  close() {
    this.drawer?.close();
  }

  toggle() {
    if (this.drawer?.opened) {
      this.drawer.close();
    } else {
      this.drawer.open();
    }
  }
}
