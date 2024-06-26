﻿using Messenger.Application.DataTransferObjects.Messages;
using Messenger.Application.Extensions;
using Messenger.Application.Mappers;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Infrastructure.Repositories.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MessageService(
            IMessageRepository messageRepository,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            _messageRepository = messageRepository;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public async ValueTask<MessageViewModel> CreateMessageAsync(MessageCreationDTO messageCreationDTO)
        {
            var filePath = await UploadFileIfItIsNotNullAsync(messageCreationDTO.MediaFile);

            var message = new Message()
            {
                SenderId = messageCreationDTO.SenderId,
                ChatId = messageCreationDTO.ChatId,
                Text = messageCreationDTO.Text,
                ParentId = messageCreationDTO.ParentId,
                FilePath = filePath,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                message = await _messageRepository.InsertAsync(message);
            }
            catch (Exception ex)
            {
                throw new ValidationException("message is not valid", ex);
            }

            message = await _messageRepository.SelectByIdWithDetailsAsync(
                expression: x => x.Id == message.Id,
                includes: new string[] { nameof(Message.Parent), nameof(Message.Sender) });

            return message.ToMessageViewModel();
        }

        private async ValueTask<string> UploadFileIfItIsNotNullAsync(IFormFile? mediaFile)
        {
            if (mediaFile is null)
                return null;

            var fileName = Guid.NewGuid().ToString() + "." + Path.GetExtension(mediaFile.FileName);

            var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, fileName);

            using (var stream = File.OpenRead(fullPath))
            {
                await mediaFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public ValueTask<MessageViewModel> ModifyMessageAsync(MessageModificationDTO messageModificationDTO)
            => throw new NotImplementedException();

        public async ValueTask<MessageViewModel> RemoveMessageAsync(Guid messageId)
        {
            var message = await _messageRepository.SelectByIdAsync(messageId);

            if (message is null)
                throw new NotFoundException("Message not found");

            message = await _messageRepository.DeleteAsync(message);

            return message.ToMessageViewModel();
        }

        public async ValueTask<MessageViewModel> RetrieveMessageByIdAsync(Guid messageId)
        {
            var message = await _messageRepository.SelectByIdAsync(messageId);

            if (message is null)
                throw new NotFoundException("Message not found");

            return message.ToMessageViewModel();
        }

        public List<MessageViewModel> RetrieveMessagesByChatId(QueryParameter queryParameter, Guid chatId)

        {
            var messages = _messageRepository.SelectAll()
                .Include(x => x.Sender)
                .Include(x => x.Parent)
                .Where(x => x.ChatId == chatId)
                .AsNoTracking()
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
            );

            return messages.Select(x => x.ToMessageViewModel()).ToList();
        }

        public List<MessageViewModel> RetrieveMessagesByUserId(QueryParameter queryParameter, long userId)
        {
            var messages = _messageRepository.SelectAll()
                .Include(x => x.Sender)
                .Include(x => x.Parent)
                .Where(x => x.SenderId == userId)
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            return messages.Select(x => x.ToMessageViewModel()).ToList();
        }
    }
}
