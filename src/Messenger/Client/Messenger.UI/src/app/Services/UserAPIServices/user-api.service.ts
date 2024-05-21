import { Injectable } from '@angular/core';
import { UserViewModel } from '../../Interfaces/Users/user-view-model';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from 'os';

@Injectable({
  providedIn: 'root'
})
export class UserAPIService {

  private userInfoUrl = 'https://localhost:7031/api/users';

  constructor(private http:HttpClient) { }

  getUserInfoFromAPI(){
    return this.http.get<UserViewModel>(`${this.userInfoUrl}/user-info`);
  }

  getUserInfoFromJSON(){
    var userJson = localStorage.getItem('userProfile')!;
    return JSON.parse(userJson)
  }
}
