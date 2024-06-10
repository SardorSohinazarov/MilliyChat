import { Component, Input } from '@angular/core';
import { MessageDTO } from '../../../Models/Messages/message-dto';

@Component({
  selector: 'app-text-message',
  standalone: true,
  imports: [],
  templateUrl: './text-message.component.html',
  styleUrl: './text-message.component.scss'
})
export class TextMessageComponent {
  @Input() message!:MessageDTO;
  @Input({required:true}) isMine!:boolean;
}
