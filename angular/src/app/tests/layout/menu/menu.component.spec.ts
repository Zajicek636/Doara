import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatListModule } from '@angular/material/list';
import { MenuComponent } from '../../../shared/layout/menu/menu.component';
import {MenuItemComponent} from '../../../shared/layout/menu/menu-item/menu-item.component';
import {SharedModule} from '../../../shared/shared.module';
import {By} from '@angular/platform-browser';

describe('MenuComponent', () => {
  let component: MenuComponent;
  let fixture: ComponentFixture<MenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MenuComponent, MenuItemComponent], // pokud `MenuItemComponent` existuje
      imports: [SharedModule, RouterTestingModule, MatListModule],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the menu component', () => {
    expect(component).toBeTruthy();
  });

  it('should render main menu categories', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('Sklady');
    expect(compiled.textContent).toContain('Účetnictví');
  });

  it('should expand nested items when clicked', async () => {
    // Najdi první `MenuItemComponent` (např. "Sklady" nebo "Účetnictví")
    const menuItemDebugEl = fixture.debugElement.queryAll(By.directive(MenuItemComponent))[1]; // druhý = Účetnictví
    const menuItemInstance = menuItemDebugEl.componentInstance;

    // Aktivuj, čímž se subitems zobrazí
    menuItemInstance.activate(menuItemInstance.item);
    fixture.detectChanges();

    // Nyní hledej podřízené položky
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('Seznam faktur');
    expect(compiled.textContent).toContain('Subjekty');
  });


});
