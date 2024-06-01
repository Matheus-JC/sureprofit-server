using SureProfit.Domain.Entities;
using SureProfit.Domain.ValueObjects;

namespace SureProfit.Domain.Interfaces.Data;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByCnpj(Cnpj cnpj);
}
