using SureProfit.Domain.Common;
using SureProfit.Domain.ValueObjects;

namespace SureProfit.Domain.Entities;

public class Company : Entity
{
    public string Name { get; private set; } = string.Empty;
    public Cnpj? Cnpj { get; private set; }

    private readonly List<Store> _stores = [];
    public IReadOnlyCollection<Store> Stores => _stores;

    public Company(string name)
    {
        SetName(name);
    }

    public Company(string name, string cnpj) : this(name)
    {
        Cnpj = new Cnpj(cnpj);
    }

    public void SetName(string name)
    {
        AssertionConcern.AssertArgumentNotEmpty(name, "Name cannot be empty");
        Name = name;
    }

    public void AddStore(Store store)
    {
        _stores.Add(store);
    }

    public void RemoveStore(Store store)
    {
        _stores.Remove(store);
    }
}
