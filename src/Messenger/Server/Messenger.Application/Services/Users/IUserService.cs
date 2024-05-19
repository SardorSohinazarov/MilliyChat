using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Users
{
    public interface IUserService
    {
        List<UserViewModel> RetrieveUsers(QueryParameter queryParameter);
        List<UserViewModel> RetrieveByChatIdUsers(QueryParameter queryParameter, Guid chatId);
        ValueTask<UserProfileViewModel> RetrieveUserByIdAsync(long userId);
        ValueTask<UserViewModel> ModifyUserAsync(UserModificationDTO userModificationDTO);
        ValueTask<UserViewModel> RemoveUserAsync(long userId);
    }
}
