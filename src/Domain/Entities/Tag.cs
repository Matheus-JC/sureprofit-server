using SureProfit.Domain.Common;

namespace SureProfit.Domain.Entities;

public class Tag : Entity
{
    public string Name { get; private set; } = string.Empty;
    public bool Active { get; private set; }

    public Tag(string name)
    {
        SetName(name);
        Activate();
    }

    protected Tag() { }

    public void SetName(string name)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, "Name cannot be empty");
        Name = name;
    }

    public void Activate()
    {
        Active = true;
    }

    public void Inactivate()
    {
        Active = false;
    }
}