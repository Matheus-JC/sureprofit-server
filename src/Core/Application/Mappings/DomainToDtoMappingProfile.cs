using AutoMapper;
using SureProfit.Application.CompanyManagement;
using SureProfit.Application.CostManagement;
using SureProfit.Application.StoreManagement;
using SureProfit.Application.TagManagement;
using SureProfit.Domain.Entities;

namespace SureProfit.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj != null ? src.Cnpj.Value : null));

        CreateMap<Store, StoreDto>();

        CreateMap<Store, StoreProfitSummariesDto>();

        CreateMap<Tag, TagDto>();

        CreateMap<Cost, CostDto>();
    }
}
