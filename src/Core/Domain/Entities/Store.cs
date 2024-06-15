using SureProfit.Domain.Common;
using SureProfit.Domain.Enums;

namespace SureProfit.Domain.Entities;

public class Store : Entity
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public StoreEnviroment Enviroment { get; private set; }
    public decimal? TargetProfit { get; private set; }
    public decimal PerItemFee { get; private set; }
    private readonly List<Cost> _costs = [];
    public IReadOnlyCollection<Cost> Costs => _costs;

    public Company? Company { get; private set; }

    public Store(Guid companyId, string name, StoreEnviroment enviroment)
    {
        SetCompanyId(companyId);
        SetName(name);
        SetEnviroment(enviroment);
    }

    protected Store() { }

    public void SetName(string name)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, $"'Name' cannot be empty");
        Name = name;
    }

    public void SetTargetProfit(decimal targetProfit)
    {
        AssertionConcern.AssertArgumentRange(targetProfit, 0, 100, "'Target Profit' is percentage and must be between 0 and 100");
        TargetProfit = targetProfit;
    }

    public void SetPerItemFee(decimal perItemFee)
    {
        AssertionConcern.AssertArgumentMinimumValue(perItemFee, 0, "'Target Profit' is percentage and must be between 0 and 100");
        PerItemFee = perItemFee;
    }

    public void SetEnviroment(StoreEnviroment enviroment)
    {
        Enviroment = enviroment;
    }

    public void SetCompany(Company company)
    {
        Company = company;
        SetCompanyId(Company.Id);
    }

    public void SetCompanyId(Guid companyId)
    {
        AssertionConcern.AssertArgumentNotEquals(companyId, Guid.Empty, "'CompanyId' cannot be empty");
        CompanyId = companyId;
    }
}
