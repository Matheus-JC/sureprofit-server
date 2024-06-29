using Microsoft.EntityFrameworkCore;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;
using SureProfit.Domain.ValueObjects;

namespace SureProfit.Infraestructure.Data.Repositories;

public class CompanyRepository(ApplicationDbContext context) : Repository<Company>(context), ICompanyRepository
{
    public async Task<Company?> GetByCnpj(Cnpj cnpj)
    {
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Cnpj != null && c.Cnpj.Value == cnpj.Value);
    }
}
