using Bogus;
using FluentAssertions;
using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Enums;

namespace SureProfit.Domain.Tests.Entities;

public class CostTests
{
    private readonly Faker _faker = new("en_US");

    [Fact]
    public void SetDescription_WithValidValue_ShouldSet()
    {
        var cost = CreateCost(GetRandomCostType());
        var newDescription = GenerateRandomDescription();

        cost.SetDescription(newDescription);

        cost.Description.Should().Be(newDescription);
    }

    [Fact]
    public void SetDescription_WithEmptyValue_ShouldThrowDomainException()
    {
        var cost = CreateCost(GetRandomCostType());

        FluentActions.Invoking(() => cost.SetDescription(""))
            .Should().Throw<DomainException>().WithMessage("*empty*");
    }

    [Fact]
    public void SetValue_WhenInputLessThanZero_ShouldThrowDomainException()
    {
        var cost = CreateCost(GetRandomCostType());
        var newValue = -1;

        FluentActions.Invoking(() => cost.SetValue(newValue))
            .Should().Throw<DomainException>().WithMessage("*0*");
    }

    [Fact]
    public void SetValue_WithValidInputWhenTypeIsFixed_ShouldSet()
    {
        var cost = CreateCost(CostType.Fixed);
        var newValue = GenerateRandomValue(CostType.Fixed);

        cost.SetValue(newValue);

        cost.Value.Should().Be(newValue);
    }

    [Fact]
    public void SetValue_WithValueBiggerThanOneHundredWhenTypeIsFixed_ShouldSet()
    {
        var cost = CreateCost(CostType.Fixed);
        var newValue = 101;

        cost.SetValue(newValue);

        cost.Value.Should().Be(newValue);
    }

    [Fact]
    public void SetValue_WithZeroValueWhenTypeIsFixed_ShouldThrowDomainException()
    {
        var cost = CreateCost(CostType.Fixed);
        var newValue = 0;

        FluentActions.Invoking(() => cost.SetValue(newValue))
            .Should().Throw<DomainException>().WithMessage("*0*");
    }

    [Fact]
    public void SetValue_WithValidInputWhenTypeIsPercentage_ShouldSet()
    {
        var cost = CreateCost(CostType.Percentage);
        var newValue = GenerateRandomValue(CostType.Percentage);

        cost.SetValue(newValue);

        cost.Value.Should().Be(newValue);
    }

    [Fact]
    public void SetValue_WithZeroValueWhenTypeIsPercentage_ShouldSet()
    {
        var cost = CreateCost(CostType.Percentage);
        var newValue = 0;

        cost.SetValue(newValue);

        cost.Value.Should().Be(newValue);
    }

    [Fact]
    public void SetValue_WithValueBiggerThanOneHundredWhenTypeIsPercentage_ShouldThrowDomainException()
    {
        var cost = CreateCost(CostType.Percentage);
        var newValue = 101;

        FluentActions.Invoking(() => cost.SetValue(newValue))
            .Should().Throw<DomainException>().WithMessage("*100*");
    }

    [Fact]
    public void SetStore_WithValidInput_ShouldSet()
    {
        var cost = CreateCost(GetRandomCostType());
        var store = new Store(_faker.Company.CompanyName(), _faker.PickRandom<StoreEnviroment>());

        cost.SetStore(store);

        cost.Store.Should().Be(store);
    }

    [Fact]
    public void SetTag_WithValidInput_ShouldSet()
    {
        var cost = CreateCost(GetRandomCostType());
        var tag = new Tag(_faker.Random.String(5, 15));

        cost.SetTag(tag);

        cost.Tag.Should().Be(tag);
    }

    private Cost CreateCost(CostType costType) => new(description: GenerateRandomDescription(), GenerateRandomValue(costType), costType);
    private string GenerateRandomDescription() => _faker.Random.String(5, 15);
    private decimal GenerateRandomValue(CostType costType) =>
        costType == CostType.Fixed ? _faker.Random.Decimal(0) : _faker.Random.Decimal(0, 100);
    private CostType GetRandomCostType() => _faker.PickRandom<CostType>();
}
