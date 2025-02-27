import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { SkladyComponent } from './components/sklady.component';
import { SkladyService } from '@sklady';
import { of } from 'rxjs';

describe('SkladyComponent', () => {
  let component: SkladyComponent;
  let fixture: ComponentFixture<SkladyComponent>;
  const mockSkladyService = jasmine.createSpyObj('SkladyService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [SkladyComponent],
      providers: [
        {
          provide: SkladyService,
          useValue: mockSkladyService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkladyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
