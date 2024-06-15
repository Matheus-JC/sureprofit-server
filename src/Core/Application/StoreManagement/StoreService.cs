using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Application.StoreManagement;

public class StoreService(IStoreRepository storeRepository, ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork, INotifier notifier, IMapper mapper, IMarkupCalculator markupCalculator
)
    : BaseService(unitOfWork, notifier, mapper), IStoreService
{
    private readonly IStoreRepository _storeRepository = storeRepository;
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IMarkupCalculator _markupCalculator = markupCalculator;

    public async Task<IEnumerable<StoreDto>> GetAllAsync()
    {
        var stores = await _storeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<StoreDto>>(stores);
    }

    public async Task<StoreDto?> GetByIdAsync(Guid id)
    {
        var store = await _storeRepository.GetByIdAsync(id);

        if (store is null)
        {
            Notify("Store not found");
            return null;
        }

        return _mapper.Map<Store?, StoreDto>(store);
    }

    public async Task<IEnumerable<Cost>> GetVariableCostsByStore(Guid storeId)
    {
        var storeVariableCosts = await _storeRepository.GetVariableCostsByStore(storeId);
        return _mapper.Map<IEnumerable<Cost>>(storeVariableCosts);
    }

    public async Task<Guid> CreateAsync(StoreDto storeDto)
    {
        var validator = new StoreDtoValidator(_storeRepository, _companyRepository, validateId: false);
        if (!await Validate(validator, storeDto))
        {
            return Guid.Empty;
        }

        storeDto.Id = Guid.Empty;
        var store = _mapper.Map<Store>(storeDto);

        _storeRepository.Create(store);

        await CommitAsync();

        return store.Id;
    }

    public async Task UpdateAsync(StoreDto storeDto)
    {
        var validator = new StoreDtoValidator(_storeRepository, _companyRepository, validateId: true);
        if (!await Validate(validator, storeDto))
        {
            return;
        }

        var storeUpdate = _mapper.Map<Store>(storeDto);

        _storeRepository.Update(storeUpdate);

        await CommitAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var store = await _storeRepository.GetByIdAsync(id);

        if (store is null)
        {
            Notify("Store not found");
            return;
        }

        _storeRepository.Delete(store);

        await CommitAsync();
    }

    public async Task<decimal?> CalculateMarkupMultiplier(Guid storeId)
    {
        var store = await _storeRepository.GetByIdAsync(storeId);

        if (store is null)
        {
            Notify("Store not found");
            return null;
        }

        return await _markupCalculator.Calculate(storeId);
    }

    public void Dispose()
    {
        _storeRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
