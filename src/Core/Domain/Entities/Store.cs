using SureProfit.Domain.Common;
using SureProfit.Domain.Enums;

namespace SureProfit.Domain.Entities;

public class Store : Entity
{
    public string Name { get; private set; } = string.Empty;
    public decimal? TargetProfit { get; private set; }
    public StoreEnviroment Enviroment { get; private set; }

    public Store(string name, StoreEnviroment enviroment)
    {
        SetName(name);
        SetEnviroment(enviroment);
    }

    public Store(string name, StoreEnviroment enviroment, decimal targetProfit) : this(name, enviroment)
    {
        SetTargetProfit(targetProfit);
    }

    public void SetName(string name)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, "Name cannot be empty");
        Name = name;
    }

    public void SetTargetProfit(decimal targetProfit)
    {
        AssertionConcern.AssertArgumentRange(targetProfit, 0, 100, "Target Profit is percentage and must be between 0 and 100");
        TargetProfit = targetProfit;
    }

    public void SetEnviroment(StoreEnviroment enviroment)
    {
        Enviroment = enviroment;
    }
}
