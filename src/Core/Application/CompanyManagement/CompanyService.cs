using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application.CompanyManagement;

public class CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork,
    INotifier notifier, IMapper mapper, ICompanyUniquenessChecker companyUniquenessChecker
)
    : BaseService(unitOfWork, notifier, mapper), ICompanyService
{
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly ICompanyUniquenessChecker _companyUniquenessChecker = companyUniquenessChecker;

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
        if (!await Validate(new CompanyDtoValidator(_companyRepository, validateId: false), companyDto))
        {
            return Guid.Empty;
        }

        companyDto.Id = Guid.Empty;
        var company = _mapper.Map<Company>(companyDto);

        if (company.Cnpj is not null)
        {
            await _companyUniquenessChecker.CheckCnpj(company);
        }

        _companyRepository.Create(company);

        await CommitAsync();

        return company.Id;
    }

    public async Task UpdateAsync(CompanyDto companyDto)
    {
        if (!await Validate(new CompanyDtoValidator(_companyRepository, validateId: true), companyDto))
        {
            return;
        }

        var company = _mapper.Map<Company>(companyDto);

        if (company.Cnpj is not null)
        {
            await _companyUniquenessChecker.CheckCnpj(company);
        }

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
        _companyRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
