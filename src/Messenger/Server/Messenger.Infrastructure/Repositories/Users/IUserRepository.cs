using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User, long>
    {
    }
}
