using System.Linq.Expressions;
using SureProfit.Domain.Common;

namespace SureProfit.Domain.Interfaces.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
    Task<bool> Exists(Guid Id);

    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);

    Task<int> SaveChangesAsync();
}
