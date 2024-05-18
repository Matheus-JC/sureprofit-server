using Bogus;
using FluentAssertions;
using SureProfit.Domain.Common;
using SureProfit.Domain.Entities;

namespace SureProfit.Domain.Tests.Entities;

public class TagTests
{
    private readonly Faker _faker = new("en_US");

    private Tag CreateTag() => new(name: CreateRandomName());
    private string CreateRandomName() => _faker.Random.String(5, 15);

    [Fact]
    public void SetTag_WithValidValue_ShouldSet()
    {
        var tag = CreateTag();
        var newName = CreateRandomName();

        tag.SetName(newName);

        tag.Name.Should().Be(newName);
    }

    [Fact]
    public void SetName_WithEmptyValue_ShouldThrowDomainException()
    {
        var tag = CreateTag();
        var emptyName = "";

        FluentActions.Invoking(() => tag.SetName(emptyName))
            .Should().Throw<DomainException>().WithMessage($"*empty*");
    }

    [Fact]
    public void Activate_AInactivatedTag_ShouldBecameActive()
    {
        var tag = CreateTag();

        tag.Inactivate();
        tag.Activate();

        tag.Active.Should().BeTrue();
    }

    [Fact]
    public void Deactivated_AnActivatedTag_ShouldBecameInactive()
    {
        var tag = CreateTag();

        tag.Inactivate();

        tag.Active.Should().BeFalse();
    }
}
