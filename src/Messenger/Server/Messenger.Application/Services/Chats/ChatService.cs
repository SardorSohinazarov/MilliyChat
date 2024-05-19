using Messenger.Application.DataTransferObjects.Chats;
using Messenger.Application.Extensions;
using Messenger.Application.Helpers.PasswordHasher;
using Messenger.Application.Mappers;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;
using Messenger.Domain.Entities;
using Messenger.Domain.Enums;
using Messenger.Domain.Exceptions;
using Messenger.Infrastructure.Repositories.Chats;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Messenger.Application.Services.Chats
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordHasherService _passwordHasher;

        public ChatService(
            IChatRepository chatRepository,
            IHttpContextAccessor httpContextAccessor,
            IPasswordHasherService passwordHasher)
        {
            _chatRepository = chatRepository;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
        }

        public async ValueTask<ChatViewModel> RemoveChatAsync(Guid ChatId)
        {
            var chat = await _chatRepository.SelectByIdAsync(ChatId);

            if (chat is null)
                throw new NotFoundException("chat not found");

            chat = await _chatRepository.DeleteAsync(chat);

            return chat.ToChatViewModel();
        }

        public async ValueTask<ChatViewModel> RetrieveChatByIdAsync(Guid chatId)
        {
            var chat = await _chatRepository.SelectByIdWithDetailsAsync(
                expression: x => x.Id == chatId,
                includes: new string[] { nameof(Chat.Users) });

            if (chat is null)
                throw new NotFoundException("chat not found");

            return chat.ToChatViewModel();
        }

        public List<ChatViewModel> RetrieveChats(QueryParameter queryParameter)
        {
            var chats = _chatRepository.SelectAll()
                .Include(x => x.Users)
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            var chatList = chats
                .Select(x => x.ToChatViewModel()).ToList();

            return chatList;
        }

        public List<ChatViewModel> RetrieveActiveChats(QueryParameter queryParameter)
        {
            var chats = _chatRepository.SelectAll()
                .Include(x => x.Users)
                .Where(x => x.Messages.Count > 0)
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            var chatList = chats
                .Select(x => x.ToChatViewModel()).ToList();

            return chatList;
        }

        public List<ChatViewModel> RetrieveUserChats(QueryParameter queryParameter)
        {
            var userId = GetUserIdFromHttpContext();

            var chats = _chatRepository.SelectAll()
                .Include(x => x.Users)
                .Where(x => x.Users.Select(x => x.UserId).Contains(userId))
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            var chatList = chats
                .Select(x => x.ToChatViewModel()).ToList();

            return chatList;
        }

        public List<ChatViewModel> RetrieveUserActiveChats(QueryParameter queryParameter)
        {
            var userId = GetUserIdFromHttpContext();

            var chats = _chatRepository.SelectAll()
                .Include(x => x.Users)
                .Where(x => x.Messages.Count > 0 && x.Users.Select(x => x.UserId).Contains(userId))
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            var chatList = chats
                .Select(x => x.ToChatViewModel()).ToList();

            return chatList;
        }

        public ValueTask<ChatViewModel> ModifyChatAsync(ChatModificationDTO chatModificationDTO)
            => throw new NotImplementedException();

        public async ValueTask<Guid> CreatePersonalChatAsync(PersonalChatCreationDTO personalChatCreationDTO)
        {
            var userId = GetUserIdFromHttpContext();
            var link = CreateLink(personalChatCreationDTO.userId, userId);

            return await CreateChatAsync(
                chatType: ChatType.OneToOne,
                link: link);
        }

        public async ValueTask<Guid> CreateGroupChatAsync(GroupChatCreationDTO groupChatCreationDTO)
        {
            return await CreateChatAsync(
                chatType: ChatType.Group,
                title: groupChatCreationDTO.title);
        }

        public async ValueTask<Guid> CreateChannelChatAsync(ChannelChatCreationDTO channelChatCreationDTO)
        {
            return await CreateChatAsync(
                chatType: ChatType.Channel,
                title: channelChatCreationDTO.title);
        }

        public async ValueTask<ChatViewModel> ClearChatMessagesAsync(Guid chatId)
        {
            var chat = await _chatRepository.SelectByIdWithDetailsAsync(
                expression: x => x.Id == chatId,
                includes: new string[] { nameof(Chat.Messages) });

            if (chat is null)
                throw new NotFoundException("chat not found");

            chat.Messages.Clear();
            await _chatRepository.SaveChangesAsync();

            return chat.ToChatViewModel();
        }

        private async ValueTask<Guid> CreateChatAsync(
            ChatType chatType,
            string? title = null,
            string? link = null)
        {
            Chat chat = null;
            if (link is not null)
            {
                chat = await _chatRepository
                    .SelectByIdWithDetailsAsync(
                        expression: user => user.Link == link,
                        includes: Array.Empty<string>());

                if (chat is not null)
                {
                    return chat.Id;
                }
            }

            chat = new Chat()
            {
                Link = link,
                Type = chatType,
                Title = title,
                CreatedAt = DateTime.UtcNow,
                OwnerId = chatType == ChatType.OneToOne ? null : GetUserIdFromHttpContext()
            };

            try
            {
                chat = await _chatRepository.InsertAsync(chat);
            }
            catch (Exception ex)
            {
                throw new ValidationException("chat is not valid, error occurred adding", ex);
            }

            return chat.Id;
        }

        private string CreateLink(long user1Id, long user2Id)
        {
            var kichik = user1Id < user2Id ? user1Id : user2Id;
            var katta = user1Id > user2Id ? user1Id : user2Id;
            var linkstring = _passwordHasher.Encrypt(kichik.ToString(), katta.ToString());

            return linkstring;
        }

        private long GetUserIdFromHttpContext()
        {
            var stringValue = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (stringValue is null)
                throw new ValidationException("Can not get userId from HttpContext");

            return long.Parse(stringValue);
        }
    }
}
