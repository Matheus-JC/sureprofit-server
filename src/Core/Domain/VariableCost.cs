using SureProfit.Domain.Common;

namespace SureProfit.Domain;

public class VariableCost : Entity
{
    public string Description { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public Tag? Tag { get; private set; }

    public VariableCost(string description, decimal value)
    {
        SetDescription(description);
        SetValue(value);
    }

    public VariableCost(string description, decimal value, Tag tag) : this(description, value)
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
        AssertionConcern.AssertArgumentRange(value, 0, 100, "Value is percentage and must be between 0 and 100");
        Value = value;
    }

    public void SetTag(Tag tag)
    {
        Tag = tag;
    }
}
