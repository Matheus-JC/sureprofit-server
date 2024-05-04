﻿using Bogus;
using Bogus.Extensions;
using FluentAssertions;

namespace SureProfit.Domain.Tests;

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
}