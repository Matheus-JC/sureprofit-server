using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application.Tests;

public class CompanyServiceTests : ServiceBaseTests
{
    private readonly CompanyService _companyService;

    public CompanyServiceTests()
    {
        _companyService = Mocker.CreateInstance<CompanyService>();
    }

    [Fact]
    public async void GetById_WithValidInput_ShouldReturnExpectedResult()
    {
        // Arrange
        var company = CreateCompany();
        var repositoryMock = Mocker.GetMock<ICompanyRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(company.Id))
            .ReturnsAsync(company);

        // Act
        var result = await _companyService.GetByIdAsync(company.Id);

        // Assert
        result.Should().Be(company);
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

        repositoryMock.Setup(repo => repo.GetByIdAsync(companyDto.Id))
            .ReturnsAsync(default(Company));

        repositoryMock.Setup(repo => repo.Create(
            It.Is<Company>(comp =>
                comp.Id == companyDto.Id
                && comp.Name == companyDto.Name
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

    [Fact]
    public async void CreateAsync_WithCompanyIdAlreadyRegistered_ShouldNotifyError()
    {
        // Arrange
        var companyDto = CreateCompanyDto();
        Mocker.GetMock<ICompanyRepository>().Setup(service => service.GetByIdAsync(companyDto.Id))
            .ReturnsAsync(new Company(companyDto.Name));
        Mocker.GetMock<INotifier>().Setup(n => n.GetNotifications())
            .Returns([It.IsAny<Notification>()]);

        // Act
        await _companyService.CreateAsync(companyDto);
        var test = Mocker.GetMock<INotifier>().Object;

        // Assert
        Mocker.GetMock<IUnitOfWork>().Verify(wow => wow.CommitAsync(), Times.Never);
        Mocker.GetMock<INotifier>().Object.GetNotifications().Count.Should().Be(1);
    }

    private CompanyDto CreateCompanyDto(bool withCnpj = true) => new()
    {
        Id = Guid.NewGuid(),
        Name = Faker.Company.CompanyName(),
        Cnpj = withCnpj ? CreateFakeCnpj() : null
    };

    private Company CreateCompany() => new(
        Faker.Company.CompanyName(),
        CreateFakeCnpj()
    );

    private string CreateFakeCnpj() => Faker.Company.Cnpj(includeFormatSymbols: false);
}
