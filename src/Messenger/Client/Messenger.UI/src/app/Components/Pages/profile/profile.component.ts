import { Component, OnInit } from '@angular/core';
import { UserAPIService } from '../../../Services/UserAPIServices/user-api.service';
import { UserProfileViewModel } from '../../../Interfaces/Users/user-profile-view-model';
import { UserViewModel } from '../../../Interfaces/Users/user-view-model';
import { error } from 'console';
import { FormBuilder, FormControlName, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@microsoft/signalr';

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
  userProfile!:UserProfileViewModel;
  userId!:number;

  profileForm: FormGroup = this.formBuilder.group({
    file: null
  });

  constructor(
    private userAPIService:UserAPIService,
    private formBuilder:FormBuilder,
    private http:HttpClient
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

  status: "initial" | "uploading" | "success" | "fail" = "initial"; // Variable to store file status
  file: File | null = null; // Variable to store file

  // On file Select
  onChange(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.status = "initial";
      this.file = file;
    }
  }

  onUpload() {
    // // we will implement this method later
    // if (this.file) {
    //   const formData = new FormData();
  
    //   formData.append('file', this.file, this.file.name);
  
    //   const upload$ = this.http.post("https://httpbin.org/post", formData);
  
    //   this.status = 'uploading';
  
    //   upload$.subscribe({
    //     next: () => {
    //       this.status = 'success';
    //     },
    //     error: (error: any) => {
    //       this.status = 'fail';
    //       console.log(error.message);
    //     },
    //   });
    // }
  }
}
