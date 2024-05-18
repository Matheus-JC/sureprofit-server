using AutoMapper;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Repositories;

namespace SureProfit.Application;

public class CompanyService(ICompanyRepository companyRepository, IMapper mapper) : ICompanyService
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Company> GetByIdAsync(Guid id)
    {
        return await _companyRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(CompanyDto companyDto)
    {
        var entity = _mapper.Map<Company>(companyDto);
        await _companyRepository.CreateAsync(entity);
    }
}
