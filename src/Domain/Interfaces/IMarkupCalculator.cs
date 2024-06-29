namespace SureProfit.Domain.Interfaces;

public interface IMarkupCalculator
{
    public Task<decimal> Calculate(Guid storeId);
}
