using Bogus;
using FluentAssertions;
using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Enums;

namespace SureProfit.Domain.UnitTests.Entities;

public class StoreTests
{
    private readonly Faker _faker = new("en_US");

    private Store CreateStore() => new(companyId: Guid.NewGuid(), name: CreateRandomName(), enviroment: CreateRandomEnviroment());
    private string CreateRandomName() => _faker.Company.CompanyName();
    private StoreEnviroment CreateRandomEnviroment() => _faker.PickRandom<StoreEnviroment>();

    [Fact]
    public void SetName_WithValidValue_ShouldSet()
    {
        var store = CreateStore();
        var newName = CreateRandomName();

        store.SetName(newName);

        store.Name.Should().Be(newName);
    }

    [Fact]
    public void SetName_WithEmptyName_ShouldThrowDomainException()
    {
        var store = CreateStore();

        FluentActions.Invoking(() => store.SetName(""))
            .Should().Throw<DomainException>().WithMessage($"*empty*");
    }

    [Fact]
    public void SetTargetProfit_WithValidPercentageValu_ShouldSet()
    {
        var store = CreateStore();
        var targetProfit = _faker.Random.Decimal(0, 100);

        store.SetTargetProfit(targetProfit);

        store.TargetProfit.Should().Be(targetProfit);
    }

    [Fact]
    public void SetTargetProfit_WithValueLessThanZero_ShouldThrowDomainException()
    {
        var store = CreateStore();
        var targetProfit = -1;

        FluentActions.Invoking(() => store.SetTargetProfit(targetProfit))
            .Should().Throw<DomainException>().WithMessage($"*percentage*");

    }

    [Fact]
    public void SetTargetProfit_WithValueGreaterThanOneHundred_ShouldThrowDomainException()
    {
        var store = CreateStore();
        var targetProfit = 101;

        FluentActions.Invoking(() => store.SetTargetProfit(targetProfit))
            .Should().Throw<DomainException>().WithMessage($"*percentage*");

    }

    [Fact]
    public void SetEnviroment_WithValidValue_ShouldSet()
    {
        var store = CreateStore();
        var enviroment = CreateRandomEnviroment();

        store.SetEnviroment(enviroment);

        store.Enviroment.Should().Be(enviroment);
    }

    [Fact]
    public void SetCompany_WithValidInput_ShouldSetCompanyAndCompanyId()
    {
        var store = CreateStore();
        var company = new Company(_faker.Company.CompanyName());

        store.SetCompany(company);

        store.Company.Should().Be(company);
    }
}