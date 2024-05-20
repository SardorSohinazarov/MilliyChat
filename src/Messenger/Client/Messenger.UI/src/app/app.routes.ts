import { Routes } from '@angular/router';
import { ChatComponent } from './Components/Pages/chat/chat.component';
import { LoginComponent } from './Components/Pages/auth/login/login.component';
import { RegisterComponent } from './Components/Pages/auth/register/register.component';

export const routes: Routes = [
    {
        path:'auth',
        children:[
            {path:'login',title:"Login | Milliy chat",component:LoginComponent},
            {path:'register',title:"Register | Milliy chat",component:RegisterComponent}
        ]
    },
    {path:'chat',component:ChatComponent},
    {path:'**',redirectTo:'/chat',pathMatch:'full'}
];
