using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Domain.Services;

public class VariableCostTotalRangeChecker(IStoreRepository storeRepository) : IVariableCostTotalRangeChecker
{
    private readonly IStoreRepository _storeRepository = storeRepository;
    public async Task Check(Guid storeId, Cost cost)
    {
        var variableCosts = await _storeRepository.GetVariableCostsByStore(storeId);
        var totalValueOtherCosts = variableCosts.Where(c => c.Id != cost.Id).Sum(x => x.Value);
        var newTotalValue = totalValueOtherCosts + cost.Value;
        AssertionConcern.AssertArgumentMaximumValue(newTotalValue, 100, "The total value of the cost variable cannot exceed 100%");
    }
}
