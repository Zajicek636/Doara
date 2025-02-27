import { TestBed } from '@angular/core/testing';
import { SkladyService } from './services/sklady.service';
import { RestService } from '@abp/ng.core';

describe('SkladyService', () => {
  let service: SkladyService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(SkladyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
