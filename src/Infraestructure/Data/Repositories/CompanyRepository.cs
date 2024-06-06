using Microsoft.EntityFrameworkCore;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;
using SureProfit.Domain.ValueObjects;
using SureProfit.Infra.Data.Repositories;

namespace SureProfit.Infra.Data;

public class CompanyRepository(ApplicationDbContext context) : Repository<Company>(context), ICompanyRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Company?> GetByCnpj(Cnpj cnpj)
    {
        return await _context.Companies.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Cnpj != null && c.Cnpj.Value == cnpj.Value);
    }
}
