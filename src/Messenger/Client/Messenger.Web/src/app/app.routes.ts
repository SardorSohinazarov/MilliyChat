import { Routes } from '@angular/router';
import { NotFoundComponent } from './Components/Pages/not-found/not-found.component';
import { HomeComponent } from './Components/Pages/home/home.component';
import { ChatComponent } from './Components/chat/chat.component';
import { LoginComponent } from './Components/Pages/Authentication/login/login.component';
import { RegisterComponent } from './Components/Pages/Authentication/register/register.component';

const projectName = 'Milliy chat';

export const routes: Routes = [
    {path:'',redirectTo:'/chats',pathMatch:'full'},
    {
        path:'chats',
        title:`Chats | ${projectName}`,
        component:HomeComponent,
        children:[
            {path:':chatId',component:ChatComponent}
        ]
    },
    {
        path:'auth',
        title:`Auth | ${projectName}`,
        children:[
            {path:'login',component:LoginComponent},
            {path:'register',component:RegisterComponent}
        ]
    },
    {path:'**',title:`not found | ${projectName}`, component:NotFoundComponent},
];
