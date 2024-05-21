using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Messenger.Infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ApplicationDbContext appDbContext;

        public BaseRepository(ApplicationDbContext appDbContext) =>
            this.appDbContext = appDbContext;

        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await appDbContext
                .AddAsync(entity);

            await SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<TEntity> SelectAll() =>
            appDbContext.Set<TEntity>();

        public async ValueTask<TEntity> SelectByIdAsync(TKey id) =>
            await appDbContext.Set<TEntity>().FindAsync(id);

        public async ValueTask<TEntity> SelectByIdWithDetailsAsync(
            Expression<Func<TEntity, bool>> expression,
            string[] includes = null)
        {
            IQueryable<TEntity> entities = SelectAll();

            foreach (var include in includes)
            {
                entities = entities.Include(include);
            }

            return await entities.FirstOrDefaultAsync(expression);
        }

        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = appDbContext
                .Update(entity);

            await SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async ValueTask<TEntity> DeleteAsync(TEntity entity)
        {
            var entityEntry = appDbContext
                .Set<TEntity>()
                .Remove(entity);

            await SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async ValueTask<int> SaveChangesAsync() =>
            await appDbContext.SaveChangesAsync();
    }
}
