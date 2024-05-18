using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Repositories;

namespace SureProfit.Application.Tests;

public class CompanyServiceTests(ServiceFixture fixture) : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _fixture = fixture;

    [Fact]
    public async void GetById_WithValidInput_ShouldReturnExpectedResult()
    {
        // Arrange
        var companyService = CreateCompanyService();
        var company = CreateCompany();
        var repositoryMock = _fixture.Mocker.GetMock<ICompanyRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(company.Id))
            .ReturnsAsync(company);

        // Act
        var result = await companyService.GetByIdAsync(company.Id);

        // Assert
        result.Should().Be(company);
        repositoryMock.Verify(r => r.GetByIdAsync(company.Id), Times.Once);
    }

    [Fact]
    public async void CreateAsync_WhenCompanyHasCnpj_ShouldCreateCorrectly()
    {
        // Arrange
        var companyService = CreateCompanyService();
        var companyDto = CreateCompanyDto(withCnpj: true);

        // Act
        await companyService.CreateAsync(companyDto);

        // Assert
        _fixture.Mocker.GetMock<ICompanyRepository>().Verify(repo => repo.CreateAsync(
            It.Is<Company>(company =>
                company.Name == companyDto.Name
                && company.Cnpj!.Value == companyDto.Cnpj
            )
        ), Times.Once);
    }

    [Fact]
    public async void CreateAsync_WhenCompanyDoesNotHaveCnpj_ShouldCreateCorrectly()
    {
        // Arrange
        var companyService = CreateCompanyService();
        var companyDto = CreateCompanyDto(withCnpj: false);

        // Act
        await companyService.CreateAsync(companyDto);

        // Assert
        _fixture.Mocker.GetMock<ICompanyRepository>().Verify(repo => repo.CreateAsync(
            It.Is<Company>(company =>
                company.Name == companyDto.Name
                && company.Cnpj == null
            )
        ), Times.Once);
    }

    private CompanyService CreateCompanyService() => _fixture.Mocker.CreateInstance<CompanyService>();

    private CompanyDto CreateCompanyDto(bool withCnpj) => new()
    {
        Name = _fixture.Faker.Company.CompanyName(),
        Cnpj = withCnpj ? CreateFakeCnpj() : null
    };

    private Company CreateCompany() => new(
        _fixture.Faker.Company.CompanyName(),
        CreateFakeCnpj()
    );

    private string CreateFakeCnpj() => _fixture.Faker.Company.Cnpj(includeFormatSymbols: false);
}
