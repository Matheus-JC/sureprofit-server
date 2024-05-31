using SureProfit.Domain.Common;

namespace SureProfit.Domain.Entities;

public class Cost : Entity
{
    public string Description { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public CostType Type { get; private set; }
    public Tag? Tag { get; private set; }
    public Store? Store { get; private set; }

    public Cost(string description, decimal value, CostType type)
    {
        Type = type;
        SetDescription(description);
        SetValue(value);
    }

    public Cost(string description, decimal value, CostType type, Tag tag) : this(description, value, type)
    {
        SetTag(tag);
    }

    protected Cost() { }

    public void SetDescription(string description)
    {
        AssertionConcern.AssertArgumentNotEmpty(description, "Description cannot be empty");
        Description = description;
    }

    public void SetValue(decimal value)
    {
        if (Type == CostType.Percentage)
        {
            AssertionConcern.AssertArgumentRange(value, 0, 100, "Value is percentage and must be between 0 and 100");
        }
        else
        {
            AssertionConcern.AssertArgumentGreaterThanZero(value, "Value cannot be less than 0");
        }

        Value = value;
    }

    public void SetStore(Store store)
    {
        Store = store;
    }

    public void SetTag(Tag tag)
    {
        Tag = tag;
    }
}
