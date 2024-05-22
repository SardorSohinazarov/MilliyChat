import { Routes } from '@angular/router';
import { ChatComponent } from './Components/Pages/Chats/chat/chat.component';
import { LoginComponent } from './Components/Pages/auth/login/login.component';
import { RegisterComponent } from './Components/Pages/auth/register/register.component';
import path from 'path';
import { title } from 'process';
import { combineLatest } from 'rxjs';
import { ChatListComponent } from './Components/Pages/Chats/chat-list/chat-list.component';
import { UserListComponent } from './Components/Pages/Users/user-list/user-list.component';
import { UserProfileComponent } from './Components/Pages/Users/user-profile/user-profile.component';

const projectName:string = 'Milliy chat';
export const routes: Routes = [
    {
        path:'auth',
        title:`Authorization | ${projectName}`,
        children:[
            {path:'login',title:`Login | ${projectName}`,component:LoginComponent},
            {path:'register',title:`Register | ${projectName}`,component:RegisterComponent},
            {path:'**',redirectTo:'/chats',pathMatch:'full'}
        ]
    },
    {
        path:'chats',
        title:`Chats | ${projectName}`,
        children:[
            {path:'',title:`Chats | ${projectName}`,component:ChatListComponent },
            {path:':chatId',title:`Chat | ${projectName}`,component:ChatComponent },
            {path:'**',redirectTo:'/chats',pathMatch:'full'}
        ]
    },
    {
        path:'users',
        title:`Users | ${projectName}`,
        children:[
            {path:'',title:`Users | ${projectName}`,component:UserListComponent },
            {path:':userId',title:`User | ${projectName}`,component:UserProfileComponent },
        ]
    },
    {path:'',redirectTo:'/chats',pathMatch:'full'},
    {path:'**',redirectTo:'/chats',pathMatch:'full'}
];
