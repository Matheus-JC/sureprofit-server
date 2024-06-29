using SureProfit.Domain.Entities;

namespace SureProfit.Application.StoreManagement;

public interface IStoreService : IDisposable
{
    Task<IEnumerable<StoreDto>> GetAllAsync();
    Task<StoreDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<Cost>> GetVariableCostsByStoreAsync(Guid storeId);
    Task<IEnumerable<StoreProfitSummariesDto>> GetStoresProfitSumariesAsync();

    Task<Guid> CreateAsync(StoreDto storeDto);
    Task UpdateAsync(StoreDto storeDto);
    Task DeleteAsync(Guid id);
    Task<decimal?> CalculateMarkupMultiplier(Guid storeId);
}
