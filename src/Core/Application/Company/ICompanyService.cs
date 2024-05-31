using SureProfit.Domain.Entities;

namespace SureProfit.Application;

public interface ICompanyService : IDisposable
{
    Task<CompanyDto?> GetByIdAsync(Guid id);
    Task<Guid?> CreateAsync(CompanyDto company);
}
