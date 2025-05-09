import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import {SkladyMainComponent} from '../../../sklady/sklady-main/sklady-main.component';
describe('SkladyMainComponent', () => {
  let component: SkladyMainComponent;
  let fixture: ComponentFixture<SkladyMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, SkladyMainComponent], // standalone komponenta
    }).compileComponents();

    fixture = TestBed.createComponent(SkladyMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create SkladyMainComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should contain <router-outlet>', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('router-outlet')).not.toBeNull();
  });
});
