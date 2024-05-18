using AutoMapper;
using SureProfit.Domain.Entities;

namespace SureProfit.Application;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<CompanyDto, Company>()
            .ConstructUsing(src => src.Cnpj != null ? new(src.Name, src.Cnpj) : new(src.Name));
    }
}
