using Microsoft.EntityFrameworkCore;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Enums;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Infra.Data.Repositories;

public class StoreRepositoy(ApplicationDbContext context) : Repository<Store>(context), IStoreRepository
{
    public async Task<IEnumerable<Cost>> GetVariableCostsByStore(Guid storeId)
    {
        return await _context.Costs.AsNoTracking().Where(c => c.Type == CostType.Percentage).ToListAsync();
    }
};

