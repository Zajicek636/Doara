import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { By } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import {HeaderComponent} from '../../../shared/layout/header/header.component';
import {SharedModule} from '../../../shared/shared.module';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let routerSpy: jasmine.SpyObj<Router>;
  let oauthSpy: jasmine.SpyObj<OAuthService>;

  beforeEach(async () => {
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    oauthSpy = jasmine.createSpyObj('OAuthService', ['logOut']);

    await TestBed.configureTestingModule({
      declarations: [HeaderComponent],
      imports: [SharedModule],
      providers: [
        { provide: Router, useValue: routerSpy },
        { provide: OAuthService, useValue: oauthSpy }
      ],
      schemas: [NO_ERRORS_SCHEMA] // ignoruje neznámé komponenty jako mat-icon
    }).compileComponents();

    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should emit toggleLeft on menu button click', () => {
    spyOn(component.toggleLeft, 'emit');
    const button = fixture.debugElement.queryAll(By.css('button'))[0];
    button.triggerEventHandler('click');
    expect(component.toggleLeft.emit).toHaveBeenCalledWith(component.toggleLeft);
  });

  it('should navigate to home on home button click', () => {
    const button = fixture.debugElement.queryAll(By.css('button'))[1];
    button.triggerEventHandler('click');
    expect(routerSpy.navigate).toHaveBeenCalledWith(['']);
  });

  it('should call logout on logout button click', () => {
    const button = fixture.debugElement.queryAll(By.css('button'))[2];
    button.triggerEventHandler('click');
    expect(oauthSpy.logOut).toHaveBeenCalled();
  });
});
