using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;
using SureProfit.Infra.Data.Repositories;

namespace SureProfit.Infra.Data;

public class CompanyRepository(ApplicationDbContext context) : Repository<Company>(context), ICompanyRepository;
