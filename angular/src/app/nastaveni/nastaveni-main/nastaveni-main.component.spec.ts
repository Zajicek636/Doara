import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NastaveniMainComponent } from './nastaveni-main.component';

describe('NastaveniMainComponent', () => {
  let component: NastaveniMainComponent;
  let fixture: ComponentFixture<NastaveniMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NastaveniMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NastaveniMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
