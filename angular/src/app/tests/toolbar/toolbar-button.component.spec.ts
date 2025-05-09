import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import {ToolbarButtonComponent} from '../../shared/context-toolbar/toolbar-button/toolbar-button.component';
import {ToolbarButton} from '../../shared/context-toolbar/context-toolbar.interfaces';
import {SharedModule} from '../../shared/shared.module';


describe('ToolbarButtonComponent', () => {
  let fixture: ComponentFixture<ToolbarButtonComponent>;
  let component: ToolbarButtonComponent;
  let actionSpy: jasmine.Spy;

  const testBtn: ToolbarButton<any> = {
    id: 'testBtn',
    text: 'Click me',
    icon: 'add',
    tooltip: 'Add something',
    class: 'primary',
    visible: true,
    disabled: false,
    width: 150,
    action: jasmine.createSpy('actionSpy')
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ToolbarButtonComponent],
      imports: [SharedModule]
    }).compileComponents();

    fixture = TestBed.createComponent(ToolbarButtonComponent);
    component = fixture.componentInstance;
    component.btn = testBtn;
    component.contextItem = { name: 'test' };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render icon and text', () => {
    const icon = fixture.debugElement.query(By.css('mat-icon'));
    const text = fixture.debugElement.query(By.css('.button-text'));
    expect(icon.nativeElement.textContent.trim()).toBe('add');
    expect(text.nativeElement.textContent.trim()).toBe('Click me');
  });

  it('should call action on click', () => {
    const actionSpy = jasmine.createSpy('actionSpy');

    component.btn = {
      id: 'btn1',
      text: 'Click',
      icon: 'add',
      tooltip: '',
      class: '',
      visible: true,
      disabled: false,
      width: 100,
      action: actionSpy
    };

    component.contextItem = { name: 'test' };
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('button'));
    button.nativeElement.click();

    expect(actionSpy).toHaveBeenCalledOnceWith({ name: 'test' });
  });

  it('should be disabled if btn.disabled is true', () => {
    component.btn.disabled = true;
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    expect(button.disabled).toBeTrue();
  });

  it('should be hidden if btn.visible is false', () => {
    component.btn.visible = false;
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    expect(button.style.visibility).toBe('hidden');
  });
});
