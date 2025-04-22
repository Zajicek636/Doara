import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovaFakturaComponent } from './nova-faktura.component';

describe('NovaFakturaComponent', () => {
  let component: NovaFakturaComponent;
  let fixture: ComponentFixture<NovaFakturaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NovaFakturaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NovaFakturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
