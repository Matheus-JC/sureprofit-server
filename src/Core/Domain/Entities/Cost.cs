using SureProfit.Domain.Common;

namespace SureProfit.Domain.Entities;

public class Cost : Entity
{
    public string Description { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public CostType Type { get; private set; }

    public Guid? TagId { get; private set; }
    public Tag? Tag { get; private set; }

    public Guid StoreId { get; private set; }
    public Store? Store { get; private set; }

    public Cost(Guid storeId, string description, decimal value, CostType type)
    {
        Type = type;
        SetDescription(description);
        SetValue(value);
        SetStoredId(storeId);
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
        SetStoredId(store.Id);
    }

    public void SetStoredId(Guid storeId)
    {
        AssertionConcern.AssertArgumentNotEquals(storeId, Guid.Empty, "Store Id cannot be empty");
        StoreId = storeId;
    }

    public void SetTag(Tag tag)
    {
        Tag = tag;
        SetTagId(tag.Id);
    }

    public void SetTagId(Guid tagId)
    {
        AssertionConcern.AssertArgumentNotEquals(tagId, Guid.Empty, "Tag Id cannot be empty");
        TagId = tagId;
    }
}
