using AutoMapper;
using SureProfit.Domain.Entities;

namespace SureProfit.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj != null ? src.Cnpj.Value : null));

        CreateMap<Store, StoreDto>();

        CreateMap<Tag, TagDto>();
    }
}
