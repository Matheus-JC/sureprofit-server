﻿using AutoMapper;
using Bogus;
using Moq.AutoMock;
using SureProfit.Application.Mappings;

namespace SureProfit.Application.UnitTests;

public abstract class ServiceBaseTests : IDisposable
{
    protected readonly Faker Faker = new("en_US");
    protected readonly AutoMocker Mocker = new();
    protected readonly IMapper Mapper;

    protected ServiceBaseTests()
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
