using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Messenger.Application.Services.Users
{
    public interface IUserService
    {
        List<UserViewModel> RetrieveUsers(QueryParameter queryParameter);
        List<UserViewModel> RetrieveByChatIdUsers(QueryParameter queryParameter, Guid chatId);
        ValueTask<UserProfileViewModel> RetrieveUserByIdAsync(long userId);
        ValueTask<UserViewModel> RetrieveUserByIdAsync();
        ValueTask<UserViewModel> ModifyUserAsync(UserModificationDTO userModificationDTO);
        ValueTask<UserViewModel> RemoveUserAsync(long userId);
        ValueTask<string> UploadProfileImageAsync(IFormFile formFile);
    }
}
