import {inject} from '@angular/core';
import {LoadingService} from './loading.service';
import {finalize} from 'rxjs';
import {HttpInterceptorFn} from '@angular/common/http';
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService);
  loadingService.show();
  return next(req).pipe(
    finalize(() => loadingService.hide())
  );
};


