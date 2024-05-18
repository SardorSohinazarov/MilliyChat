using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Users
{
    public interface IUserService
    {
        IQueryable<UserViewModel> RetrieveUsers(QueryParameter queryParameter);
        ValueTask<UserViewModel> RetrieveUserByIdAsync(long userId);
        ValueTask<UserViewModel> ModifyUserAsync(UserModificationDTO userModificationDTO);
        ValueTask<UserViewModel> RemoveUserAsync(long userId);
    }
}
