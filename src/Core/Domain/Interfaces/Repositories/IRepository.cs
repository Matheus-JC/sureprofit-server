using System.Linq.Expressions;
using SureProfit.Domain.Common;

namespace SureProfit.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<IEnumerable<TEntity?>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
