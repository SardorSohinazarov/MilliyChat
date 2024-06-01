import { Component, Input } from '@angular/core';
import { MessageDTO } from '../../../Interfaces/Message/message-dto';
import { DatePipe } from '@angular/common';
import { environment } from '../../../../environments/environment';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-text-message',
  standalone: true,
  imports: [DatePipe,RouterLink],
  providers:[DatePipe],
  templateUrl: './text-message.component.html',
  styleUrl: './text-message.component.scss'
})
export class TextMessageComponent {
  @Input() message!:MessageDTO;
  @Input({required:true}) isMine!:boolean;

  constructor() { }

  getProfilePhotoFullPath(path:any){
    if(path == null){
      return 'https://freesvg.org/img/abstract-user-flat-4.png';
    }
    else{
      return environment.apiUrl + path;
    }
  }
}
