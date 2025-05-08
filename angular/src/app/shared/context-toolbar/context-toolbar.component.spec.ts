import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ContextToolbarComponent } from './context-toolbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDividerModule } from '@angular/material/divider';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';
import { ToolbarButton } from './context-toolbar.interfaces';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {ToolbarButtonComponent} from './toolbar-button/toolbar-button.component';

describe('ContextToolbarComponent', () => {
  let component: ContextToolbarComponent<any>;
  let fixture: ComponentFixture<ContextToolbarComponent<any>>;

  const mockButtons: ToolbarButton[] = [
    {
      id: 'btn1',
      text: 'Uložit',
      icon: 'save',
      class: 'primary',
      tooltip: 'Uložit záznam',
      disabled: false,
      visible: true,
      action: jasmine.createSpy('saveAction')
    },
    {
      id: 'btn2',
      text: 'Smazat',
      icon: 'delete',
      class: 'warn',
      tooltip: 'Smazat záznam',
      disabled: false,
      visible: true,
      action: jasmine.createSpy('deleteAction')
    }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContextToolbarComponent, ToolbarButtonComponent],
      imports: [MatToolbarModule, MatDividerModule, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(ContextToolbarComponent);
    component = fixture.componentInstance;
    component.buttons = mockButtons;
    component.contextItem = { name: 'TestItem' };
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should render all toolbar buttons', () => {
    const buttons = fixture.debugElement.queryAll(By.css('app-toolbar-button'));
    expect(buttons.length).toBe(2);
  });

  it('should pass contextItem to toolbar buttons', () => {
    const btnComponent = fixture.debugElement.queryAll(By.directive(ToolbarButtonComponent))[0].componentInstance;
    expect(btnComponent.contextItem).toEqual({ name: 'TestItem' });
  });

  it('should show text by default if width fits', () => {
    component.showText = true;
    fixture.detectChanges();

    const btnText = fixture.debugElement.query(By.css('.button-text'));
    expect(btnText).toBeTruthy();
  });

  it('should hide text if overflow is detected', () => {
    // Simuluj úzký prostor pro toolbar
    spyOnProperty(component['elRef'].nativeElement.querySelector('mat-toolbar'), 'scrollWidth', 'get').and.returnValue(500);
    spyOnProperty(component['elRef'].nativeElement.querySelector('mat-toolbar'), 'clientWidth', 'get').and.returnValue(400);

    component['checkSize']();
    expect(component.showText).toBeFalse();
  });
});
