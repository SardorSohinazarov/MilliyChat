using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Messenger.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    {
        private readonly ApplicationDbContext appDbContext;

        public BaseRepository(ApplicationDbContext appDbContext) =>
            this.appDbContext = appDbContext;

        public async ValueTask<TEntity> InsertAsync(
            TEntity entity)
        {
            var entityEntry = await this.appDbContext
                .AddAsync<TEntity>(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<TEntity> SelectAll() =>
            this.appDbContext.Set<TEntity>();

        public async ValueTask<TEntity> SelectByIdAsync(TKey id) =>
            await this.appDbContext.Set<TEntity>().FindAsync(id);

        public async ValueTask<TEntity> SelectByIdWithDetailsAsync(
            Expression<Func<TEntity, bool>> expression,
            string[] includes = null)
        {
            IQueryable<TEntity> entities = this.SelectAll();

            foreach (var include in includes)
            {
                entities = entities.Include(include);
            }

            return await entities.FirstOrDefaultAsync(expression);
        }

        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = this.appDbContext
                .Update<TEntity>(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async ValueTask<TEntity> DeleteAsync(TEntity entity)
        {
            var entityEntry = this.appDbContext
                .Set<TEntity>()
                .Remove(entity);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async ValueTask<int> SaveChangesAsync() =>
            await this.appDbContext
                .SaveChangesAsync();
    }
}
