using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;
using SureProfit.Infra.Data.Repositories;

namespace SureProfit.Infra.Data;

public class StoreRepositoy(ApplicationDbContext context) : Repository<Store>(context), IStoreRepository;
