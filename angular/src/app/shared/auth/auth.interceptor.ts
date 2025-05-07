import {HttpInterceptorFn} from '@angular/common/http';

export const requestVerificationInterceptor: HttpInterceptorFn = (req, next) => {
  const cloned = req.clone({
    withCredentials: true,
    setHeaders: {
      'Authorization': 'Bearer ' + sessionStorage.getItem('access_token'),
      'X-Requested-With': 'XMLHttpRequest',
    }
  });
  return next(cloned);
};
