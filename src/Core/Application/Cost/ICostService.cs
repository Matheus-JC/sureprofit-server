namespace SureProfit.Application;

public interface ICostService : IDisposable
{
    Task<IEnumerable<CostDto>> GetAllAsync();
    Task<CostDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CostDto costDto);
    Task UpdateAsync(CostDto costDto);
    Task DeleteAsync(Guid id);
}
