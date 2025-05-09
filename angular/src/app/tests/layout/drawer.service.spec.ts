import { TestBed } from '@angular/core/testing';

import { MatSidenav } from '@angular/material/sidenav';
import { ViewContainerRef, TemplateRef } from '@angular/core';
import {DrawerService} from '../../shared/layout/drawer.service';

describe('DrawerService', () => {
  let service: DrawerService;
  let sidenavSpy: jasmine.SpyObj<MatSidenav>;
  let viewContainerRefSpy: jasmine.SpyObj<ViewContainerRef>;

  beforeEach(() => {
    sidenavSpy = jasmine.createSpyObj('MatSidenav', ['open', 'close']);
    viewContainerRefSpy = jasmine.createSpyObj('ViewContainerRef', ['clear', 'createEmbeddedView']);

    TestBed.configureTestingModule({
      providers: [DrawerService]
    });

    service = TestBed.inject(DrawerService);
    service.setDrawer(sidenavSpy);
    service.setContainerRef(viewContainerRefSpy);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should open drawer', () => {
    service.open();
    expect(sidenavSpy.open).toHaveBeenCalled();
  });

  it('should close drawer', () => {
    service.close();
    expect(sidenavSpy.close).toHaveBeenCalled();
  });

  it('should toggle drawer: open -> close', () => {
    sidenavSpy.opened = true;
    service.toggle();
    expect(sidenavSpy.close).toHaveBeenCalled();
  });

  it('should toggle drawer: closed -> open', () => {
    sidenavSpy.opened = false;
    service.toggle();
    expect(sidenavSpy.open).toHaveBeenCalled();
  });

  it('should open drawer with template', () => {
    const mockTemplate = {} as TemplateRef<any>;
    service.openWithTemplate(mockTemplate);
    expect(viewContainerRefSpy.clear).toHaveBeenCalled();
    expect(viewContainerRefSpy.createEmbeddedView).toHaveBeenCalledWith(mockTemplate);
    expect(sidenavSpy.open).toHaveBeenCalled();
  });
});
