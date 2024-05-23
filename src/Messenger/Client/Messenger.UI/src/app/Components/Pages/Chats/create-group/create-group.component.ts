import { Component } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ChatAPIService } from '../../../../Services/ChatAPIServices/chat-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-group',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './create-group.component.html',
  styleUrl: './create-group.component.scss'
})
export class CreateGroupComponent {
  chatForm: FormGroup = this.formBuilder.group({
    title: ['', Validators.required]
  });

  constructor(
    private formBuilder:FormBuilder,
    private chatAPIService:ChatAPIService,
    private router:Router
  ){}

  CreateGroup(){
    this.chatAPIService.createGroup(this.chatForm.value).subscribe(
      (result:string) =>{
        this.router.navigate([`/chats/${result}`])
      },
      error =>{
        console.log("Group yaratishda:" + error.message);
      }
    )
  }
}
