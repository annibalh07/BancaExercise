using BpInterface.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BpInterface.Infrastructure.Repositories
{
    public class BpRepository<TEntity> : IDbRepository<TEntity> where TEntity : class
    {
        protected readonly BpContext Context;
        private bool _disposed;

        public BpRepository(BpContext dbContext)
        {
            Context = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAllIQueryable()
        {
            return Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entity = Context.Set<TEntity>().Where(predicate);
            return entity;
        }

        public async Task<TEntity> GetAsync(int recordId)
        {
            TEntity entity = await Context.Set<TEntity>().FindAsync(recordId);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            List<TEntity> entities = await Context.Set<TEntity>().ToListAsync();
            return entities;
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BpRepository()
        {
            Dispose(false);
        }
    }
}
