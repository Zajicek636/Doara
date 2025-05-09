import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Router } from '@angular/router';
import { By } from '@angular/platform-browser';
import { Component } from '@angular/core';
import {HomeComponent} from '../../shared/home/home.component';
import {CommonModule} from '@angular/common';

// Dummy Router
class RouterStub {
  navigate = jasmine.createSpy('navigate');
}

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let router: RouterStub;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeComponent, CommonModule],
      providers: [{ provide: Router, useClass: RouterStub }]
    }).compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router) as any;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should render welcome message', () => {
    const h1 = fixture.debugElement.query(By.css('h1')).nativeElement;
    expect(h1.textContent).toContain('Vítej v systému Doara');
  });

  it('should navigate to "ucetnictvi" when Faktury card clicked', () => {
    const fakturyCard = fixture.debugElement.queryAll(By.css('.dashboard-card'))[0];
    fakturyCard.triggerEventHandler('click');
    expect(router.navigate).toHaveBeenCalledWith(['ucetnictvi']);
  });

  it('should navigate to "sklady" when Sklad card clicked', () => {
    const skladCard = fixture.debugElement.queryAll(By.css('.dashboard-card'))[1];
    skladCard.triggerEventHandler('click');
    expect(router.navigate).toHaveBeenCalledWith(['sklady']);
  });
});
