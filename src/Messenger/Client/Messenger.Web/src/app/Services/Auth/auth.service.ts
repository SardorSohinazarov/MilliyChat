import { Injectable } from '@angular/core';
import { environment } from '../../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { TokenDTO } from '../../Models/Auth/token-dto';
import { LoginDTO } from '../../Models/Auth/login-dto';
import { RegisterDTO } from '../../Models/Auth/register-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}api/Authentication`;

  constructor(private http: HttpClient) {}

  register(userData: RegisterDTO) {
    return this.http.post<TokenDTO>(`${this.apiUrl}/Register`, userData);
  }

  refreshToken(refreshTokenData: any) {
    return this.http.post<TokenDTO>(`${this.apiUrl}/RefreshToken`, refreshTokenData);
  }

  login(loginData: LoginDTO) {
    return this.http.post<TokenDTO>(`${this.apiUrl}/Login`, loginData);
  }

  logOut(){
    window.localStorage.clear();
  }

  getAccessToken(){
    return localStorage.getItem('accessToken');
  }

  getRefreshToken(){
    return localStorage.getItem('refreshToken');
  }

  storeToken(token:TokenDTO){
    localStorage.setItem('accessToken', token.accessToken);
    localStorage.setItem('refreshToken', token.refreshToken);
    localStorage.setItem('expireDate', token.expireDate.toString());
  }

  isLoggedIn():boolean{
    return !! localStorage.getItem('accessToken');
  }
}
