import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import {ContextToolbarComponent} from '../../shared/context-toolbar/context-toolbar.component';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {SharedModule} from '../../shared/shared.module';
import {ToolbarButtonComponent} from '../../shared/context-toolbar/toolbar-button/toolbar-button.component';

describe('ContextToolbarComponent', () => {
  let fixture: ComponentFixture<ContextToolbarComponent<any>>;
  let component: ContextToolbarComponent<any>;

  const testButtons: ToolbarButton[] = [
    {
      id: 'btn1',
      text: 'First',
      icon: 'home',
      visible: true,
      disabled: false,
      action: jasmine.createSpy('actionSpy')
    },
    {
      id: 'btn2',
      text: 'Second',
      icon: 'add',
      visible: true,
      disabled: false,
      action: jasmine.createSpy('actionSpy2')
    }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
      ],
      imports: [SharedModule]
    }).compileComponents();

    fixture = TestBed.createComponent(ContextToolbarComponent);
    component = fixture.componentInstance;
    component.buttons = testButtons;
    component.contextItem = { name: 'Item' };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render buttons inside mat-toolbar', () => {
    const toolbar = fixture.debugElement.query(By.css('mat-toolbar'));
    expect(toolbar).toBeTruthy();

    const buttonComponents = fixture.debugElement.queryAll(By.directive(ToolbarButtonComponent));
    expect(buttonComponents.length).toBe(2);
  });

  it('should not render toolbar if buttons is empty', () => {
    component.buttons = [];
    fixture.detectChanges();

    const toolbar = fixture.debugElement.query(By.css('mat-toolbar'));
    expect(toolbar).toBeNull();
  });

  it('should hide text when scrollWidth > clientWidth', () => {
    const mockToolbar = document.createElement('div');

    Object.defineProperty(mockToolbar, 'scrollWidth', { value: 300 });
    Object.defineProperty(mockToolbar, 'clientWidth', { value: 100 });

    spyOn(component['elRef'].nativeElement, 'querySelector').and.returnValue(mockToolbar);

    component['checkSize']();
    expect(component.showText).toBeFalse();
  });

  it('should show text when scrollWidth <= clientWidth', () => {
    const mockToolbar = document.createElement('div');

    Object.defineProperty(mockToolbar, 'scrollWidth', { value: 100 });
    Object.defineProperty(mockToolbar, 'clientWidth', { value: 150 });

    spyOn(component['elRef'].nativeElement, 'querySelector').and.returnValue(mockToolbar);

    component['checkSize']();
    expect(component.showText).toBeTrue();
  });
});
