using System.Linq.Expressions;
using SureProfit.Domain.Common;

namespace SureProfit.Domain.Interfaces.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<IEnumerable<TEntity?>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

    void Create(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);

    Task<int> SaveChangesAsync();
}
