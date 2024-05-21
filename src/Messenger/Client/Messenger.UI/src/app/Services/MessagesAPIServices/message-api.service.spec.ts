import { TestBed } from '@angular/core/testing';

import { MessageAPIService } from './message-api.service';

describe('MessageAPIService', () => {
  let service: MessageAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MessageAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
