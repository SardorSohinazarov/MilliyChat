using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Users
{
    public class UserService : IUserService
    {
        public ValueTask<UserViewModel> ModifyUserAsync(UserModificationDTO userModificationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<UserViewModel> RemoveUserAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<UserViewModel> RetrieveUserByIdAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserViewModel> RetrieveUsers(QueryParameter queryParameter)
        {
            throw new NotImplementedException();
        }
    }
}
