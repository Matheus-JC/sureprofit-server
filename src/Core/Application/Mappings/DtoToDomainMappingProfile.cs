using AutoMapper;
using SureProfit.Domain.Entities;

namespace SureProfit.Application;

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
    }
}
