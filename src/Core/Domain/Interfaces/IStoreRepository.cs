using SureProfit.Domain.Entities;

namespace SureProfit.Domain.Interfaces;

public interface IStoreRepository : IRepository<Store>
{
    Task<IEnumerable<Cost>> GetVariableCostsByStore(Guid storeId);
}