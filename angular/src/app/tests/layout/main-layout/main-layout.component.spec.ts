import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Component } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { NO_ERRORS_SCHEMA, ViewContainerRef } from '@angular/core';
import {MainLayoutComponent} from '../../../shared/layout/main-layout/main-layout.component';
import {DrawerService} from '../../../shared/layout/drawer.service';

@Component({
  template: `<app-main-layout></app-main-layout>`,
  standalone: false
})
class TestHostComponent {}

describe('MainLayoutComponent', () => {
  let component: MainLayoutComponent;
  let fixture: ComponentFixture<MainLayoutComponent>;
  let drawerService: jasmine.SpyObj<DrawerService>;

  beforeEach(async () => {
    drawerService = jasmine.createSpyObj('DrawerService', ['setDrawer', 'setContainerRef']);

    await TestBed.configureTestingModule({
      declarations: [MainLayoutComponent],
      imports: [
        MatSidenavModule,
        RouterTestingModule
      ],
      providers: [
        { provide: DrawerService, useValue: drawerService }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(MainLayoutComponent);
    component = fixture.componentInstance;

    // Fake drawer + outlet setup
    const fakeDrawer = jasmine.createSpyObj('MatSidenav', ['toggle']);
    component.leftDrawer = fakeDrawer;
    component.drawer = fakeDrawer;
    component.outlet = {} as ViewContainerRef;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle left drawer and update collapsed state', () => {
    const toggleSpy = spyOn(component.leftDrawer, 'toggle');
    component.leftMenuCollapsed = true;

    component.onLeftDrawerClick(null);

    expect(toggleSpy).toHaveBeenCalled();
    expect(component.leftMenuCollapsed).toBeFalse();
  });

  it('should set drawer and containerRef in AfterViewInit', () => {
    component.ngAfterViewInit();
    expect(drawerService.setDrawer).toHaveBeenCalledWith(component.drawer);
    expect(drawerService.setContainerRef).toHaveBeenCalledWith(component.outlet);
  });
});
