using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Domain.Services;

public class CompanyUniquenessChecker(ICompanyRepository companyRepository) : ICompanyUniquenessChecker
{
    private readonly ICompanyRepository _companyRepository = companyRepository;

    public async Task CheckCnpj(Company company)
    {
        if (company.Cnpj is null)
        {
            return;
        }

        var companySearched = await _companyRepository.GetByCnpj(company.Cnpj);

        if (companySearched is not null && companySearched.Id != company.Id)
        {
            throw new DomainException("CNPJ has already been registered with another company");
        }
    }
}
