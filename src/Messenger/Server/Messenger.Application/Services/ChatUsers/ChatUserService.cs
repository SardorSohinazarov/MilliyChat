using Mapster;
using Messenger.Application.DataTransferObjects.ChatUsers;
using Messenger.Application.Extensions;
using Messenger.Application.Models;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Infrastructure.Repositories.ChatUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Messenger.Application.Services.ChatUsers
{
    public class ChatUserService : IChatUserService
    {
        private readonly IChatUserRepository _chatUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatUserService(
            IChatUserRepository chatUserRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _chatUserRepository = chatUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async ValueTask<ChatUser> AddChatMemberAsync(ChatUserCreationDTO chatUserCreationDTO)
            => await CreateChatUserAsync(chatUserCreationDTO);

        public async ValueTask<ChatUser> BlockChatAsync(Guid chatId)
        {
            var userId = GetUserIdFromHttpContext();
            var chatUserModificationDTO = new ChatUserModificationDTO()
            {
                ChatId = chatId,
                UserId = userId,
                IsBlocked = true
            };

            return await ModifyChatUserAsync(chatUserModificationDTO);
        }

        public async ValueTask<ChatUser> BlockChatMemberAsync(ChatUserDTO chatUserDTO)
        {
            var chatUserModificationDTO = chatUserDTO.Adapt<ChatUserModificationDTO>();
            chatUserModificationDTO.IsBlocked = true;

            return await ModifyChatUserAsync(chatUserModificationDTO);
        }

        public async ValueTask<ChatUser> CreateChatUserAsync(ChatUserCreationDTO chatUserCreationDTO)
        {
            var chatUser = chatUserCreationDTO.Adapt<ChatUser>();

            return await _chatUserRepository.InsertAsync(chatUser);
        }

        public List<ChatUser> GetChatUsers(QueryParameter queryParameter)
        {
            var chatUsers = _chatUserRepository.SelectAll()
                .Include(x => x.User)
                .Include(x => x.Chat)
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            return chatUsers.ToList();
        }

        public async ValueTask<ChatUser> JoinChatAsync(Guid chatId)
        {
            var userId = GetUserIdFromHttpContext();

            var chatUserCreationDTO = new ChatUserCreationDTO()
            {
                ChatId = chatId,
                UserId = userId
            };

            return await CreateChatUserAsync(chatUserCreationDTO);
        }

        public async ValueTask<ChatUser> KickMemberAsync(ChatUserDTO chatUserDTO)
        {
            var chatUser = await _chatUserRepository.SelectByIdWithDetailsAsync(
                expression: x => x.ChatId == chatUserDTO.ChatId && x.UserId == chatUserDTO.UserId,
                includes: new string[] { });

            if (chatUser is null)
                throw new NotFoundException("Chat user not found");

            return await _chatUserRepository.DeleteAsync(chatUser);
        }

        public async ValueTask<ChatUser> LeaveChatAsync(Guid chatId)
        {
            var userId = GetUserIdFromHttpContext();

            var chatUser = await _chatUserRepository.SelectByIdWithDetailsAsync(
                expression: x => x.ChatId == chatId && x.UserId == userId,
                includes: new string[] { });

            if (chatUser is null)
                throw new NotFoundException("Chat user not found");

            return await _chatUserRepository.DeleteAsync(chatUser);
        }

        public async ValueTask<ChatUser> ModifyChatUserAsync(ChatUserModificationDTO chatUserModificationDTO)
        {
            var chatUser = chatUserModificationDTO.Adapt<ChatUser>();

            return await _chatUserRepository.UpdateAsync(chatUser);
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
