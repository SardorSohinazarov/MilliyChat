import { Injectable } from '@angular/core';
import { UserViewModel } from '../../Interfaces/Users/user-view-model';
import { HttpClient } from '@angular/common/http';
import { UserProfileComponent } from '../../Components/Pages/Users/user-profile/user-profile.component';
import { UserProfileViewModel } from '../../Interfaces/Users/user-profile-view-model';

@Injectable({
  providedIn: 'root'
})
export class UserAPIService {

  private userInfoUrl = 'https://localhost:7031/api/users';
  //https://localhost:7031/api/users/1

  constructor(private http:HttpClient) { }

  getUserInfoFromAPI(){
    return this.http.get<UserViewModel>(`${this.userInfoUrl}/user-info`);
  }

  getUserInfoFromJSON(){
    var userJson = localStorage.getItem('userProfile')!;
    return JSON.parse(userJson)
  }

  getAllUsers(pageIndex:number = 1){
    return this.http.get<UserViewModel[]>(`${this.userInfoUrl}?Page.Index=${pageIndex}`)
  }

  getUserProfile(userId:number){
    return this.http.get<UserProfileViewModel>(`${this.userInfoUrl}/${userId}`);
  }
}
