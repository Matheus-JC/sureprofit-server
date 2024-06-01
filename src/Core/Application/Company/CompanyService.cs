using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork,
    INotifier notifier, IMapper mapper, ICnpjUniquenessChecker cnpjUniquenessChecker
)
    : BaseService(unitOfWork, notifier, mapper), ICompanyService
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly ICnpjUniquenessChecker _cnpjUniquenessChecker = cnpjUniquenessChecker;

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<CompanyDto?> GetByIdAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<Guid> CreateAsync(CompanyDto companyDto)
    {
        if (!Validate(new CompanyDtoValidator(), companyDto))
        {
            return Guid.Empty;
        }

        var company = _mapper.Map<Company>(companyDto);

        if (company.Cnpj is not null)
        {
            await _cnpjUniquenessChecker.Check(company.Cnpj);
        }

        _companyRepository.Create(company);

        await CommitAsync();

        return company.Id;
    }

    public async Task UpdateAsync(CompanyDto companyDto)
    {
        if (!Validate(new CompanyDtoValidator(), companyDto))
        {
            return;
        }

        var company = _mapper.Map<Company>(companyDto);
        _companyRepository.Update(company);

        await CommitAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);

        if (company is null)
        {
            Notify("Company not found");
            return;
        }

        _companyRepository.Delete(company);

        await CommitAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
