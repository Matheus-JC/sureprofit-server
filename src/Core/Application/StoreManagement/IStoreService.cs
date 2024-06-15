namespace SureProfit.Application.StoreManagement;

public interface IStoreService : IDisposable
{
    Task<IEnumerable<StoreDto>> GetAllAsync();
    Task<StoreDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(StoreDto storeDto);
    Task UpdateAsync(StoreDto storeDto);
    Task DeleteAsync(Guid id);
}
