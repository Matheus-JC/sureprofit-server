using SureProfit.Domain.Entities;

namespace SureProfit.Application;

public interface ICompanyService
{
    Task<Company> GetByIdAsync(Guid id);
    Task CreateAsync(CompanyDto company);
}
