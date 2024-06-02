import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../Services/AuthServices/auth.service';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';
import { TokenDTO } from '../Interfaces/Auth/token-dto';
import { RefreshTokenDTO } from '../Interfaces/Auth/refresh-token-dto';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const myToken = authService.getAccessToken();
  console.log(myToken)
  const router = inject(Router);

  console.log(req.url);

  if(myToken){
    req = req.clone({
      setHeaders:({Authorization:`Bearer ${myToken}`})
    })
  }
  return next(req).pipe(
    catchError((err:any)=>{
      if(err instanceof HttpErrorResponse){
        if(err.status === 401){
          let token:RefreshTokenDTO = {
            accessToken:authService.getAccessToken()!,
            refreshToken:authService.getRefreshToken()!
          };

          return authService.refreshToken(token)
            .pipe(
              switchMap((data:TokenDTO) => {
                authService.storeToken(data);
                req = req.clone({
                  setHeaders:({Authorization:`Bearer ${data.accessToken}`})
                })
                return next(req)
              }),
              catchError((err) =>{
                return throwError(()=>{
                  alert("Token expired");
                  window.localStorage.clear();
                  router.navigate(['/auth/login'])
                })
              })
            )
        }
      }
      return throwError(() => new Error("Nimadur xato ketdida"));
    })
  );
};
