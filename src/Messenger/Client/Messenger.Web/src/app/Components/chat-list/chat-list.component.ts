import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { catchError, of } from 'rxjs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { RouterLink } from '@angular/router';
import { Chat } from '../../Models/Chats/chat';
import { ChatService } from '../../Services/Chats/chat.service';

@Component({
  selector: 'app-chat-list',
  standalone: true,
  imports: [
    NzListModule,
    NzSkeletonModule,
    RouterLink
  ],
  templateUrl: './chat-list.component.html',
  styleUrl: './chat-list.component.scss'
})


export class ChatListComponent implements OnInit{
  initLoading = true;
  loadingMore = false;
  list: Chat[] = [];
  pageIndex = 1;

  constructor(
    private http: HttpClient,
    private chatService:ChatService
  ) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    this.chatService.getUserChats()
      .pipe(
        catchError(() => of([]))
      )
      .subscribe((res: Chat[]) => {
        this.list = res;
        this.initLoading = false;
      });
  }

  onLoadMore(): void {
    this.loadingMore = true;

    this.chatService.getUserChats(this.pageIndex + 1)
      .pipe(
        catchError(() => of([]))
      )
      .subscribe((res: Chat[]) => {
        if(res.length == 10){
          this.pageIndex ++;
          this.list = this.list.concat(res);
        }
          // this.list = [...this.list, ...res];
          this.loadingMore = false;
      });
  }
}
