using System.Linq.Expressions;

namespace BpInterface.Infrastructure.Repositories
{
    public interface IDbRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetAsync(int recordId);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAllIQueryable();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChangesAsync();
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
