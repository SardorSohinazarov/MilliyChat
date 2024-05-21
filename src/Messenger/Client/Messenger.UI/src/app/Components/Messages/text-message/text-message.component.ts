import { Component, Input } from '@angular/core';
import { MessageDTO } from '../../../Interfaces/Message/message-dto';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-text-message',
  standalone: true,
  imports: [DatePipe],
  providers:[DatePipe],
  templateUrl: './text-message.component.html',
  styleUrl: './text-message.component.scss'
})
export class TextMessageComponent {
  @Input() message!:MessageDTO;
  @Input({required:true}) isMine!:boolean;

  constructor() { }
}
