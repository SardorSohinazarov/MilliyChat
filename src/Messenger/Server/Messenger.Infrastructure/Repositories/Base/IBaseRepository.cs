using System.Linq.Expressions;

namespace Messenger.Infrastructure.Repositories.Base
{
    public interface IBaseRepository<TEntity, TKey>
    {
        ValueTask<TEntity> InsertAsync(TEntity entity);
        IQueryable<TEntity> SelectAll();
        ValueTask<TEntity> SelectByIdAsync(TKey id);

        ValueTask<TEntity> SelectByIdWithDetailsAsync(
            Expression<Func<TEntity, bool>> expression,
            string[] includes);

        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<TEntity> DeleteAsync(TEntity entity);
        ValueTask<int> SaveChangesAsync();
    }
}
