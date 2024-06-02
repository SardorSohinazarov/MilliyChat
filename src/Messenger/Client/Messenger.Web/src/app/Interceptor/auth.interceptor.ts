import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../Services/Auth/auth.service';
import { Router } from 'express';
import { TokenDTO } from '../Models/Auth/token-dto';
import { RefreshTokenDTO } from '../Models/Auth/refresh-token-dto';
import { environment } from '../../environment/environment';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const accessToken = authService.getAccessToken();
  const refreshToken = authService.getAccessToken();

  let authReq = req;
  // Agar token mavjud bo'lsa, requestni klonlab, headerga tokenni qo'shamiz
  if (accessToken) {
    authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${accessToken}`)
    });
  }

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && accessToken && refreshToken) {

        let token:RefreshTokenDTO = {
          accessToken:authService.getAccessToken()!,
          refreshToken:authService.getRefreshToken()!
        };

        // Tokenni yangilash
        return authService.refreshToken(token).pipe(
          switchMap((response: TokenDTO) => {
            authService.storeToken(response)

            // Yangi token bilan so'rovni qayta amalga oshirish
            const newAuthReq = req.clone({
              headers: req.headers.set('Authorization', `Bearer ${response.accessToken}`)
            });

            return next(newAuthReq);
          })
        );
      } else {
        return throwError(() => new Error("Nimadur xato ketdida"));
      }
    })
  );
};
