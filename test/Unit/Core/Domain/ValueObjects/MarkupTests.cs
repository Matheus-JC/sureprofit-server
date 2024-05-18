using Bogus;
using FluentAssertions;
using SureProfit.Domain.Common;
using SureProfit.Domain.ValueObjects;

namespace SureProfit.Domain.Tests;

public class MarkupTests
{
    [Fact]
    public void CreateMarkup_WithValueGreaterThanZero_ShouldCreate()
    {
        decimal value = 10.00m;

        var markup = new Markup(value);

        markup.Value.Should().Be(value);
    }

    [Fact]
    public void CreateMarkup_WithValueLessThanZero_ShouldThrowDomainException()
    {
        decimal value = -1;

        FluentActions.Invoking(() => new Markup(value))
            .Should().Throw<DomainException>().WithMessage("*0*");
    }
}
