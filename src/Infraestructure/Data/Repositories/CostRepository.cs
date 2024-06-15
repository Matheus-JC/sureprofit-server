using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Infra.Data.Repositories;

public class CostRepository(ApplicationDbContext context) : Repository<Cost>(context), ICostRepository;
