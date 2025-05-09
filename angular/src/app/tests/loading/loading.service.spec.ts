import { TestBed } from '@angular/core/testing';
import { take } from 'rxjs/operators';
import {LoadingService} from '../../shared/loading/loading.service';

describe('LoadingService', () => {
  let service: LoadingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoadingService]
    });
    service = TestBed.inject(LoadingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should emit true when show is called', (done) => {
    service.loading$.pipe(take(1)).subscribe((value: any) => {
      expect(value).toBe(false);
    });

    service.show();
    service.loading$.pipe(take(1)).subscribe((value: any) => {
      expect(value).toBe(true);
      done();
    });
  });

  it('should emit false when hide is called after show', (done) => {
    service.show();
    service.hide();
    service.loading$.pipe(take(1)).subscribe((value: any) => {
      expect(value).toBe(false);
      done();
    });
  });
});
