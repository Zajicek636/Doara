import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnyFormComponent } from './any-form.component';

describe('AnyFormComponent', () => {
  let component: AnyFormComponent;
  let fixture: ComponentFixture<AnyFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnyFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnyFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
