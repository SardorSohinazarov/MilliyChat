using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        public UserRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
