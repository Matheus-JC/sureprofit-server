using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Enums;
using SureProfit.Domain.Interfaces;
using SureProfit.Domain.Services;

namespace SureProfit.Domain.Tests.Services;

public class MarkupCalculatorTests
{
    private readonly AutoMocker Mocker = new();
    private readonly MarkupCalculator _markupCalculator;

    public MarkupCalculatorTests()
    {
        _markupCalculator = Mocker.CreateInstance<MarkupCalculator>();
    }

    [Fact]
    public async void Calculate_withValidInput_ShouldCalculateCorrectly()
    {
        var storeId = Guid.NewGuid();
        decimal expectedMarkupMultiplier = 1.33m;

        Mocker.GetMock<IStoreRepository>()
            .Setup(c => c.GetVariableCostsByStore(It.IsAny<Guid>()))
            .ReturnsAsync(GenerateVariableCosts());

        var markupMultiplier = await _markupCalculator.Calculate(storeId);

        markupMultiplier.Should().Be(expectedMarkupMultiplier);
    }

    private static List<Cost> GenerateVariableCosts()
    {
        var variableCosts = new Faker<Cost>("en_US")
            .CustomInstantiator(f => new Cost(
                storeId: Guid.NewGuid(),
                description: f.Lorem.Random.String2(10),
                value: 5.0m,
                type: CostType.Percentage
            ));

        return variableCosts.Generate(5);
    }
}
