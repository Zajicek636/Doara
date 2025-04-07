import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UcetnictviMainComponent } from './ucetnictvi-main.component';

describe('UcetnictviMainComponent', () => {
  let component: UcetnictviMainComponent;
  let fixture: ComponentFixture<UcetnictviMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UcetnictviMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UcetnictviMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
