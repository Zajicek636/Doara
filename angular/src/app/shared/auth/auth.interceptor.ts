import {HttpInterceptorFn} from '@angular/common/http';

export const requestVerificationInterceptor: HttpInterceptorFn = (req, next) => {
  const cloned = req.clone({
    withCredentials: true,
    setHeaders: {
      'RequestVerificationToken': 'CfDJ8LuzrFk3NkxAjWXyEuR-MFhZTQcwgCoVTYOYZU9O3_ePjBvNrNh_9j0U7rfsegn7zb6_wLwddnsJrCHr_xmd5fmrA_up_f1_C7YqBLvMKifOt5eYEb03zWZ4fGoRqRNy7B9tXbHxIOB7tDYR62WPPu6CYbLidLN8EnHBAU83vmBjk5FQa8D5WQ_3SN4yR9LVyw',
      'X-Requested-With': 'XMLHttpRequest'
    }
  });
  return next(cloned);
};
