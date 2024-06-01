using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.Extensions;
using Messenger.Application.Mappers;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Messenger.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public ValueTask<UserViewModel> ModifyUserAsync(UserModificationDTO userModificationDTO)
            => throw new NotImplementedException();

        public ValueTask<UserViewModel> RemoveUserAsync(long userId)
            => throw new NotImplementedException();

        public List<UserViewModel> RetrieveUsers(QueryParameter queryParameter)
        {
            var userId = GetUserIdFromHttpContext();

            var users = _userRepository.SelectAll()
                .Where(x => x.Id != userId)
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            return users.Select(x => x.ToUserViewModel()).ToList();
        }

        public List<UserViewModel> RetrieveByChatIdUsers(QueryParameter queryParameter, Guid chatId)
        {
            var users = _userRepository.SelectAll()
                .Include(x => x.Chats)
                .Where(x => x.Chats.Select(x => x.Id).Contains(chatId))
                .ToPagedList(
                    httpContext: _httpContextAccessor.HttpContext,
                    pageSize: queryParameter.Page.Size,
                    pageIndex: queryParameter.Page.Index
                );

            return users.Select(x => x.ToUserViewModel()).ToList();
        }

        public async ValueTask<UserProfileViewModel> RetrieveUserByIdAsync(long userId)
        {
            var user = await _userRepository.SelectByIdWithDetailsAsync(
                expression: x => x.Id == userId,
                includes: new string[] {
                    nameof(User.Chats),
                    nameof(User.AuthorshipChats),
                    nameof(User.Messages)
                }
            );

            return user.ToUserProfileViewModel();
        }

        public async ValueTask<UserViewModel> RetrieveUserByIdAsync()
        {
            var userId = GetUserIdFromHttpContext();

            var user = await _userRepository.SelectByIdWithDetailsAsync(
                expression: x => x.Id == userId,
                includes: new string[] {
                    nameof(User.Chats),
                    nameof(User.AuthorshipChats),
                    nameof(User.Messages)
                }
            );

            return user.ToUserViewModel();
        }

        private long GetUserIdFromHttpContext()
        {
            var stringValue = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (stringValue is null)
                throw new ValidationException("Can not get userId from HttpContext");

            return long.Parse(stringValue);
        }

        public async ValueTask<string> UploadProfileImageAsync(IFormFile formFile)
        {
            var userId = GetUserIdFromHttpContext();
            var user = await _userRepository.SelectByIdAsync(userId);

            try
            {
                var fileName = Path.Combine(
                    "profile-images",
                    Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));

                var path = Path.Combine(_webHostEnvironment.WebRootPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                user.PhotoPath = fileName.Replace(@"\", @"/");
                await _userRepository.SaveChangesAsync();

                return fileName;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception on Profile image uploading", ex);
            }
        }
    }
}
