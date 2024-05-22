import { Component, OnInit } from '@angular/core';
import { UserAPIService } from '../../../../Services/UserAPIServices/user-api.service';
import { UserViewModel } from '../../../../Interfaces/Users/user-view-model';
import { error } from 'console';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent implements OnInit{
  users:UserViewModel[] = [];
  constructor(private userAPIService:UserAPIService){}

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(){
    this.userAPIService.getAllUsers().subscribe(
      (result:UserViewModel[]) =>{
        this.users = result;
      },
      error =>{
        console.log('Userlarni yuklashda xatolik:' + error.Message);
      }
    )
  }

}
