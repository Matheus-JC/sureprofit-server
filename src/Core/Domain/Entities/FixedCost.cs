using SureProfit.Domain.Common;

namespace SureProfit.Domain.Entities;

public class FixedCost : Entity
{
    public string Description { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public Tag? Tag { get; private set; }

    public FixedCost(string description, decimal value)
    {
        SetDescription(description);
        SetValue(value);
    }

    public FixedCost(string description, decimal value, Tag tag) : this(description, value)
    {
        SetTag(tag);
    }

    public void SetDescription(string description)
    {
        AssertionConcern.AssertArgumentNotEmpty(description, "Description cannot be empty");
        Description = description;
    }

    public void SetValue(decimal value)
    {
        AssertionConcern.AssertArgumentGreaterThanZero(value, "Value cannot be less than 0");
        Value = value;
    }

    public void SetTag(Tag tag)
    {
        Tag = tag;
    }
}
