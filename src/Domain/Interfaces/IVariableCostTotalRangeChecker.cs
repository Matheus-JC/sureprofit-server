using SureProfit.Domain.Entities;

namespace SureProfit.Domain.Interfaces;

public interface IVariableCostTotalRangeChecker
{
    Task Check(Guid storeId, Cost cost);
}
