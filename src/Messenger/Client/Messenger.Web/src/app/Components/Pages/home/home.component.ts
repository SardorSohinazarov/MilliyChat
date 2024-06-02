import { Component, OnInit } from '@angular/core';
import { NzResizeDirection, NzResizeEvent, NzResizeHandleOption } from 'ng-zorro-antd/resizable';
import { NzResizableModule } from 'ng-zorro-antd/resizable';
import { NzColDirective, NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule, NZ_ICONS } from 'ng-zorro-antd/icon';
import { CaretUpOutline } from '@ant-design/icons-angular/icons';
import { ChatListComponent } from '../../chat-list/chat-list.component';
import { RouterOutlet } from '@angular/router';
import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { ChatService } from '../../../Services/Chats/chat.service';
import { Chat } from '../../../Models/Chats/chat';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NzResizableModule,
    NzGridModule,
    NzIconModule,
    ChatListComponent,
    RouterOutlet
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent{
  col = 8;
  id = -1;
  directions: NzResizeHandleOption[] = [
    {
      direction: 'right',
      cursorType: 'grid'
    }
  ];

  onResize({ col }: NzResizeEvent): void {
    cancelAnimationFrame(this.id);
    this.id = requestAnimationFrame(() => {
      this.col = col!;
    });
  }

  constructor(private chatAPIService:ChatService){}
}
