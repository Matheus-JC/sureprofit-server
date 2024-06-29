using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Enums;

namespace SureProfit.Domain.UnitTests.Entities;

public class CompanyTests
{
    private readonly Faker _faker = new("en_US");

    private Company CreateCompany() => new(name: CreateRandomName());
    private string CreateRandomName() => _faker.Company.CompanyName();

    [Fact]
    public void SetName_WithValidValue_ShouldSet()
    {
        var company = CreateCompany();
        var newName = CreateRandomName();

        company.SetName(newName);

        company.Name.Should().Be(newName);
    }

    [Fact]
    public void SetName_WithEmptyName_ShouldThrowDomainException()
    {
        var company = CreateCompany();

        FluentActions.Invoking(() => company.SetName(""))
            .Should().Throw<DomainException>().WithMessage($"*empty*");
    }

    [Fact]
    public void SetCnpj_WithValidInput_ShouldSet()
    {
        var company = CreateCompany();
        var cnpj = _faker.Company.Cnpj(includeFormatSymbols: false);

        company.SetCnpj(cnpj);

        company.Cnpj!.Value.Should().Be(cnpj);
    }

    [Fact]
    public void AddStore_WithValidInput_ShouldAdd()
    {
        var company = CreateCompany();
        var store = new Store(company.Id, _faker.Company.CompanyName(), _faker.PickRandom<StoreEnviroment>());

        company.AddStore(store);

        company.Stores.First().Should().Be(store);
    }

    [Fact]
    public void RemoveStore_WithValidInput_ShouldRemove()
    {
        var company = CreateCompany();
        var store = new Store(company.Id, _faker.Company.CompanyName(), _faker.PickRandom<StoreEnviroment>());

        company.AddStore(store);

        company.Stores.First().Should().Be(store);

        company.RemoveStore(store);

        company.Stores.Should().NotContain(store);
    }
}
