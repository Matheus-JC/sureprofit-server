using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Infraestructure.Data.Repositories;

public class CostRepository(ApplicationDbContext context) : Repository<Cost>(context), ICostRepository;
