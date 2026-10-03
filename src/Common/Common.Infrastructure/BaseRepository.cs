using System.Linq.Expressions;
using Common.Domain;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : Entity where TContext : DbContext
    {
        protected TContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(ICollection<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public TEntity? Get(Guid id)
        {
            return _dbSet.FirstOrDefault(t => t.Id.Equals(id));
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<TEntity?> GetTracking(Guid id)
        {
            return await _dbSet.AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;
        }
    }
}
