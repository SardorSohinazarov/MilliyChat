import { Component, OnInit } from '@angular/core';
import { UserAPIService } from '../../../Services/UserAPIServices/user-api.service';
import { UserProfileViewModel } from '../../../Interfaces/Users/user-profile-view-model';
import { UserViewModel } from '../../../Interfaces/Users/user-view-model';
import { error } from 'console';
import { FormBuilder, FormControlName, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@microsoft/signalr';
import { HttpEventType } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    ReactiveFormsModule

  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit{
  private apiUrl = environment.apiUrl;

  userProfile!:UserProfileViewModel;
  userId!:number;

  profileForm: FormGroup = this.formBuilder.group({
    file: null
  });

  constructor(
    private userAPIService:UserAPIService,
    private formBuilder:FormBuilder
  ){}

  ngOnInit(): void {
    this.getUserId();
    this.getUserProfile();
  }

  getUserId(){
    this.userId = this.userAPIService.getUserInfoFromJSON().id;
  }

  getUserProfile(){
    this.userAPIService.getUserProfile(this.userId).subscribe(
      (result:UserProfileViewModel) =>{
        console.log(result);

        this.userProfile = result;
      },
      error =>{
        console.log('Profileni olishda xatolik:'+error.message);
      }
    )
  }

  selectedFile!: File;
  uploadedImagePath!: string;

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (!this.selectedFile) {
      return;
    }

    this.userAPIService.onSubmit(this.selectedFile)?.subscribe(
      (response:string) => {
        console.log(response);
        this.uploadedImagePath = response;
      },
      error => {
        console.log('Upload error:', error.message);
      }
    );
  }

  getPhotoFullPath(){
    return this.apiUrl +this.userProfile.photoPath;
  }
}
