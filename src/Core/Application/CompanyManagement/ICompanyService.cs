namespace SureProfit.Application.CompanyManagement;

public interface ICompanyService : IDisposable
{
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<CompanyDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CompanyDto companyDto);
    Task UpdateAsync(CompanyDto companyDto);
    Task DeleteAsync(Guid id);
}
