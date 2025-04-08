import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkladyPolozkyComponent } from './sklady-polozky.component';

describe('SkladyPolozkyComponent', () => {
  let component: SkladyPolozkyComponent;
  let fixture: ComponentFixture<SkladyPolozkyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SkladyPolozkyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SkladyPolozkyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
