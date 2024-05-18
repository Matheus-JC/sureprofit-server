using SureProfit.Domain.Common;

namespace SureProfit.Domain.ValueObjects;

public class Markup
{
    public decimal Value { get; private set; }

    public Markup(decimal value)
    {
        AssertionConcern.AssertArgumentGreaterThanZero(value, "Value cannot be less than 0");

        Value = value;
    }
}
