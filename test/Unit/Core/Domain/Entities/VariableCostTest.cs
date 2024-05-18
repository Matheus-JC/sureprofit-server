using Bogus;
using FluentAssertions;
using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;

namespace SureProfit.Domain.Tests.Entities;

public class VariableCostTest
{
    private readonly Faker _faker = new("en_US");

    private VariableCost CreateVariableCost() => new(description: GenerateRandomDescription(), GenerateRandomValue());
    private string GenerateRandomDescription() => _faker.Random.String(5, 15);
    private decimal GenerateRandomValue() => _faker.Random.Decimal(0, 100);

    [Fact]
    public void SetDescription_WithValueNotEmpty_ShouldSet()
    {
        var variableCost = CreateVariableCost();
        var newDescription = GenerateRandomDescription();

        variableCost.SetDescription(newDescription);

        variableCost.Description.Should().Be(newDescription);
    }

    [Fact]
    public void SetDescription_WithEmptyValue_ShouldThrowDomainException()
    {
        var variableCost = CreateVariableCost();

        FluentActions.Invoking(() => variableCost.SetDescription(""))
            .Should().Throw<DomainException>().WithMessage("*empty*");
    }

    [Fact]
    public void SetValue_WithValidPercentageValue_ShouldSet()
    {
        var variableCost = CreateVariableCost();
        var newValue = GenerateRandomValue();

        variableCost.SetValue(newValue);

        variableCost.Value.Should().Be(newValue);
    }

    [Fact]
    public void SetValue_WithValueLessThanZero_ShouldThrowDomainException()
    {
        var variableCost = CreateVariableCost();
        var value = -1;

        FluentActions.Invoking(() => variableCost.SetValue(value))
            .Should().Throw<DomainException>().WithMessage("*percentage*");
    }

    [Fact]
    public void SetValue_WithValueGreaterThanOneHundred_ShouldThrowDomainException()
    {
        var variableCost = CreateVariableCost();
        var value = 101;

        FluentActions.Invoking(() => variableCost.SetValue(value))
            .Should().Throw<DomainException>().WithMessage("*percentage*");
    }
}
