namespace Common.Domain.Repository;

using System.Linq.Expressions;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    public Task<TEntity?> GetAsync(Guid id);
    public Task<TEntity?> GetTracking(Guid id);
    public Task AddAsync(TEntity entity);
    public void Add(TEntity entity);
    public Task AddRange(ICollection<TEntity> entities);
    public void Update(TEntity entity);
    public Task<int> Save();
    public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression);
    public bool Exists(Expression<Func<TEntity, bool>> expression);
    public TEntity? Get(Guid id);
}
