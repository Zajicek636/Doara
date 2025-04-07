import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkladyMainComponent } from './sklady-main.component';

describe('SkladyMainComponent', () => {
  let component: SkladyMainComponent;
  let fixture: ComponentFixture<SkladyMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SkladyMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SkladyMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
