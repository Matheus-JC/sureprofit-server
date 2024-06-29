using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using SureProfit.Application.CompanyManagement;
using SureProfit.Application.Notifications;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Application.UnitTests;

public class CompanyServiceTests : ServiceBaseTests
{
    private readonly CompanyService _companyService;

    public CompanyServiceTests()
    {
        _companyService = Mocker.CreateInstance<CompanyService>();
    }

    [Fact]
    public async void GetAllAsync_ShouldReturnExpectedResult()
    {
        //Arrange
        int companiesCount = 5;
        var companies = GenerateCompanies(companiesCount);

        Mocker.GetMock<ICompanyRepository>().Setup(r => r.GetAllAsync())
            .ReturnsAsync(companies);

        // Act
        var result = await _companyService.GetAllAsync();

        // Assert
        result.Should().HaveCount(companiesCount);
        Mocker.GetMock<ICompanyRepository>().Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async void GetByIdAsync_WithValidInput_ShouldReturnExpectedResult()
    {
        // Arrange
        var company = CreateCompany();
        var repositoryMock = Mocker.GetMock<ICompanyRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(company.Id))
            .ReturnsAsync(company);

        // Act
        var result = await _companyService.GetByIdAsync(company.Id);

        // Assert
        result.Should().BeEquivalentTo(new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Cnpj = company.Cnpj?.Value,
        });

        repositoryMock.Verify(r => r.GetByIdAsync(company.Id), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void CreateAsync_WithValidInput_ShouldCreateCompany(bool withCnpj)
    {
        // Arrange
        var companyDto = CreateCompanyDto(withCnpj);
        var repositoryMock = Mocker.GetMock<ICompanyRepository>();

        repositoryMock.Setup(repo => repo.Create(
            It.Is<Company>(comp =>
                comp.Name == companyDto.Name
                && (withCnpj ? comp.Cnpj!.Value == companyDto.Cnpj : comp.Cnpj == null)
            )
        ));

        // Act
        await _companyService.CreateAsync(companyDto);

        // Assert
        repositoryMock.VerifyAll();
        Mocker.GetMock<INotifier>().Object.HasNotification().Should().BeFalse();
        Mocker.GetMock<IUnitOfWork>().Verify(wow => wow.CommitAsync(), Times.Once);
    }

    private CompanyDto CreateCompanyDto(bool withCnpj = true) => new()
    {
        Id = Guid.NewGuid(),
        Name = Faker.Company.CompanyName(),
        Cnpj = withCnpj ? CreateFakeCnpj() : null
    };

    private Company CreateCompany() => GenerateCompanies(1).First();

    private List<Company> GenerateCompanies(int quantity)
    {
        var companies = new Faker<Company>("en_US")
            .CustomInstantiator(c => new Company(
                name: Faker.Company.CompanyName(),
                cnpj: CreateFakeCnpj()
            ));

        return companies.Generate(quantity);
    }

    private string CreateFakeCnpj() => Faker.Company.Cnpj(includeFormatSymbols: false);
}
