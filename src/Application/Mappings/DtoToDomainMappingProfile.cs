﻿using AutoMapper;
using SureProfit.Application.CompanyManagement;
using SureProfit.Application.CostManagement;
using SureProfit.Application.StoreManagement;
using SureProfit.Application.TagManagement;
using SureProfit.Domain.Entities;

namespace SureProfit.Application.Mappings;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<CompanyDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != Guid.Empty))
            .ConstructUsing(src => src.Cnpj != null ? new(src.Name, src.Cnpj) : new(src.Name));

        CreateMap<StoreDto, Store>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != Guid.Empty));

        CreateMap<TagDto, Tag>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != Guid.Empty));

        CreateMap<CostDto, Cost>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != Guid.Empty));
    }
}
