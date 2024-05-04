using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;

namespace SureProfit.Domain.Tests;

public class CnpjTests
{
    private readonly Faker _faker = new("en_US");

    private string CreateRandomCnpjValue() => _faker.Company.Cnpj(includeFormatSymbols: false);

    [Fact]
    public void CreateCnpj_WithValidValue_ShouldCreate()
    {
        var cnpjValue = CreateRandomCnpjValue();

        var cnpj = new Cnpj(cnpjValue);

        cnpj.Value.Should().Be(cnpjValue);
    }

    [Fact]
    public void CreateCnpj_WithShorterLength_ShouldThrowDomainException()
    {
        var fakeCnpj = CreateRandomCnpjValue()[..^1];

        FluentActions.Invoking(() => new Cnpj(fakeCnpj))
            .Should().Throw<DomainException>().WithMessage($"*{Cnpj.CnpjLength}*");
    }

    [Fact]
    public void CreateCnpj_WithLargerLength_ShouldThrowDomainException()
    {
        var fakeCnpj = CreateRandomCnpjValue() + '0';

        FluentActions.Invoking(() => new Cnpj(fakeCnpj))
            .Should().Throw<DomainException>().WithMessage($"*{Cnpj.CnpjLength}*");
    }

    [Fact]
    public void CreateCnpj_WithInvalidVerificator_ShouldThrowDomainException()
    {
        var validCnpj = CreateRandomCnpjValue();
        var verifyingDigit = validCnpj[^1];
        var fakeCnpj = validCnpj[..^1] + (verifyingDigit == '0' ? '1' : '0');

        FluentActions.Invoking(() => new Cnpj(fakeCnpj))
            .Should().Throw<DomainException>().WithMessage($"*Invalid*");
    }
}
