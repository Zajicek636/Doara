import {Injectable, TemplateRef} from '@angular/core';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatSidenav } from '@angular/material/sidenav';
import { ViewContainerRef } from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DrawerService {
  private drawer!: MatSidenav;
  private containerRef!: ViewContainerRef;
  private drawerOpenSubject = new BehaviorSubject<boolean>(false);
  drawerOpen$ = this.drawerOpenSubject.asObservable();
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
    this.drawerOpenSubject.next(true);
  }

  open() {
    this.drawer?.open();
    this.drawerOpenSubject.next(true);
  }

  close() {
    this.drawer?.close();
    this.drawerOpenSubject.next(false);
  }

  toggle(template?: TemplateRef<any>) {
    if (this.drawer?.opened) {
      this.close();
    } else {
      if (template) {
        this.openWithTemplate(template);
      } else {
        this.open();
      }
    }
  }

  public isOpen() {
    return this.drawerOpenSubject.getValue();
  }
}
