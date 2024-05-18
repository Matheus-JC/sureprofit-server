using System.Reflection;
using AutoMapper;
using Bogus;
using Moq.AutoMock;
using SureProfit.Application.Mappings;

namespace SureProfit.Application.Tests;

public class ServiceFixture : IDisposable
{
    public readonly Faker Faker = new("en_US");
    public readonly AutoMocker Mocker = new();
    public readonly IMapper Mapper;

    public ServiceFixture()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DtoToDomainMappingProfile>();
            cfg.AddProfile<DomainToDtoMappingProfile>();
        });

        Mapper = config.CreateMapper();
        Mocker.Use(Mapper);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
