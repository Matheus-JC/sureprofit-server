using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork,
    INotifier notifier, IMapper mapper
)
    : BaseService(unitOfWork, notifier, mapper), ICompanyService
{
    private readonly ICompanyRepository _companyRepository = companyRepository;

    public async Task<CompanyDto?> GetByIdAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<Guid?> CreateAsync(CompanyDto companyDto)
    {
        if (!Validate(new CompanyDtoValidator(), companyDto))
            return null;

        var entity = _mapper.Map<Company>(companyDto);
        _companyRepository.Create(entity);

        await CommitAsync();

        return entity.Id;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
