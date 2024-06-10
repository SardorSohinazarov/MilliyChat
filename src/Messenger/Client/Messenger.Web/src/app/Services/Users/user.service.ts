import { HttpClient, HttpErrorResponse, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { UserViewModel } from '../../Models/Users/user-view-model';
import { environment } from '../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userInfoUrl = `${environment.apiUrl}api/users`;

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

  // getUserProfile(userId:number){
  //   return this.http.get<UserProfileViewModel>(`${this.userInfoUrl}/${userId}`);
  // }

  private apiUrl = 'https://localhost:7031/api/users/upload-profile-image'; // Replace with your API endpoint

  uploadFile(file: File, additionalData: any): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    
    // Append additional data
    for (const key in additionalData) {
      if (additionalData.hasOwnProperty(key)) {
        formData.append(key, additionalData[key]);
      }
    }

    return this.http.post<any>(this.apiUrl, formData, {
      reportProgress: true,
      observe: 'events'
    }).pipe(
      catchError(this.handleError)
    );
  }
  
  private handleError(error: HttpErrorResponse) {
    // Handle the error here
    return throwError(error.message || "Server Error");
  }

  onSubmit(selectedFile:any) {
    if (!selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', selectedFile);

    return this.http.post<string>('https://localhost:7031/api/users/upload-profile-image', formData);
  }
}
