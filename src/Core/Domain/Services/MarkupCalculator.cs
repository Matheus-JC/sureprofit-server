using SureProfit.Domain.Interfaces;

namespace SureProfit.Domain.Services;

public class MarkupCalculator(IStoreRepository storeRepository) : IMarkupCalculator
{
    private readonly IStoreRepository _storeRepository = storeRepository;

    public async Task<decimal> Calculate(Guid storeId)
    {
        var variableCosts = await _storeRepository.GetVariableCostsByStore(storeId);
        var totalPercentageCost = variableCosts.Sum(vc => vc.Value);

        if (totalPercentageCost > 100) throw new Exception("Total percentage cannot be greater than 100");

        return CalculateMarkupMultiplier(totalPercentageCost);
    }

    private static decimal CalculateMarkupMultiplier(decimal totalPercentageCost)
    {
        var markupMultiplier = 100 / (100 - totalPercentageCost);
        return Math.Round(markupMultiplier, 2);
    }
}
