import { Component } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ChatAPIService } from '../../../../Services/ChatAPIServices/chat-api.service';
import { Router } from '@angular/router';
import { ChannelChatCreationDTO } from '../../../../Interfaces/Chat/channel-chat-creation-dto';
import { error } from 'console';
import { title } from 'process';

@Component({
  selector: 'app-create-channel',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './create-channel.component.html',
  styleUrl: './create-channel.component.scss'
})
export class CreateChannelComponent {

  constructor(
    private formBuilder:FormBuilder,
    private chatAPIService:ChatAPIService,
    private router:Router
  ){}

  chatForm: FormGroup = this.formBuilder.group({
    title: ['', Validators.required]
  });

  CreateChannel(){
    this.chatAPIService.createChannel(this.chatForm.value).subscribe(
      (result:string) =>{
        this.router.navigate([`/chats/${result}`])
      },
      error =>{
        console.log("Channel yaratishda:" + error.message);
      }
    )
  }
}
