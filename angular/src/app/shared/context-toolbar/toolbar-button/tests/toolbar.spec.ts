import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { By } from '@angular/platform-browser';
import { Component } from '@angular/core';
import {ToolbarButtonComponent} from '../toolbar-button.component';
import {ToolbarButton} from '../../context-toolbar.interfaces';


describe('ToolbarButtonComponent', () => {
  let component: ToolbarButtonComponent<any>;
  let fixture: ComponentFixture<ToolbarButtonComponent<any>>;
  let actionSpy: jasmine.Spy;

  const testButton: ToolbarButton<any> = {
    id: 'btn1',
    text: 'Click Me',
    icon: 'add',
    tooltip: 'Add item',
    class: 'accent',
    width: 120,
    visible: true,
    disabled: false,
    action: jasmine.createSpy('actionSpy')
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatIconModule, MatTooltipModule],
      declarations: [ToolbarButtonComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(ToolbarButtonComponent);
    component = fixture.componentInstance;
    component.btn = testButton;
    component.contextItem = { name: 'ContextObj' };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display button text and icon', () => {
    const text = fixture.debugElement.query(By.css('.button-text')).nativeElement;
    const icon = fixture.debugElement.query(By.css('mat-icon')).nativeElement;

    expect(text.textContent.trim()).toBe('Click Me');
    expect(icon.textContent.trim()).toBe('add');
  });

  it('should call action with contextItem when clicked', () => {
    const button = fixture.debugElement.query(By.css('button'));
    button.triggerEventHandler('click');
    fixture.detectChanges();

    expect(testButton.action).toHaveBeenCalledOnceWith({ name: 'ContextObj' });
  });

  it('should set tooltip, width, class and visibility', () => {
    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    expect(button.getAttribute('mattooltip')).toBe('Add item');
    expect(button.style.width).toBe('120px');
    expect(button.classList).toContain('accent');
    expect(button.style.visibility).toBe('visible');
  });

  it('should disable the button if btn.disabled is true', () => {
    component.btn.disabled = true;
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    expect(button.disabled).toBeTrue();
  });

  it('should hide the button if btn.visible is false', () => {
    component.btn.visible = false;
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    expect(button.style.visibility).toBe('hidden');
  });
});
