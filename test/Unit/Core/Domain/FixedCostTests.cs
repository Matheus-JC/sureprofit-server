using Bogus;
using FluentAssertions;

namespace SureProfit.Domain.Tests;

public class FixedCostTests
{
    private readonly Faker _faker = new("en_US");

    private FixedCost CreateFixedCost() => new(description: GenerateRandomDescription(), GenerateRandomValue());
    private string GenerateRandomDescription() => _faker.Random.String(5, 15);
    private decimal GenerateRandomValue() => _faker.Random.Decimal(0.1m);

    [Fact]
    public void SetDescription_WithValidValue_ShouldSet()
    {
        var fixedCost = CreateFixedCost();
        var newDescription = GenerateRandomDescription();

        fixedCost.SetDescription(newDescription);

        fixedCost.Description.Should().Be(newDescription);
    }

    [Fact]
    public void SetDescription_WithEmptyValue_ShouldThrowDomainException()
    {
        var fixedCost = CreateFixedCost();

        FluentActions.Invoking(() => fixedCost.SetDescription(""))
            .Should().Throw<DomainException>().WithMessage("*empty*");
    }

    [Fact]
    public void SetValue_WithValueGreaterThanZero_ShouldSet()
    {
        var fixedCost = CreateFixedCost();
        var newValue = GenerateRandomValue();

        fixedCost.SetValue(newValue);

        fixedCost.Value.Should().Be(newValue);
    }

    [Fact]
    public void SetValue_WithZeroValue_ShouldThrowDomainException()
    {
        var fixedCost = CreateFixedCost();

        FluentActions.Invoking(() => fixedCost.SetValue(0))
            .Should().Throw<DomainException>().WithMessage("*0*");
    }
}
