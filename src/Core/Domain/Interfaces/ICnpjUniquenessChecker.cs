using SureProfit.Domain.Entities;

namespace SureProfit.Domain;

public interface ICompanyUniquenessChecker
{
    Task CheckCnpj(Company company);
}
