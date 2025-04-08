import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkladyEditaceComponent } from './sklady-editace.component';

describe('SkladyEditaceComponent', () => {
  let component: SkladyEditaceComponent;
  let fixture: ComponentFixture<SkladyEditaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SkladyEditaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SkladyEditaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
