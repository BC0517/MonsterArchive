import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from './auth-service';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const authToken = authService.getToken();
  const router = inject(Router);

  if(authToken){
    const authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authToken}`)
    })
    return next(authReq);
  }
  return next(req).pipe(catchError(err => {
    if(err.status === 401){
      router.navigate(['/login']);
    }
    return throwError(() => err);
  }))
};
