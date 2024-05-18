using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.ChatUsers
{
    public interface IChatUserRepository : IBaseRepository<ChatUser, Guid>
    {
    }
}
