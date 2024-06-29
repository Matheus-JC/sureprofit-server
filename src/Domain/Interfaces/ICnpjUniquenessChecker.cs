using SureProfit.Domain.Entities;

namespace SureProfit.Domain.Interfaces;

public interface ICompanyUniquenessChecker
{
    Task CheckCnpj(Company company);
}
