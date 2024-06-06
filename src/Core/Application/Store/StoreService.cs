using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application;

public class StoreService(IStoreRepository storeRepository, ICompanyRepository companyRepository, IUnitOfWork unitOfWork,
    INotifier notifier, IMapper mapper
)
    : BaseService(unitOfWork, notifier, mapper), IStoreService
{
    private readonly IStoreRepository _storeRepository = storeRepository;
    private readonly ICompanyRepository _companyRepository = companyRepository;


    public async Task<IEnumerable<StoreDto>> GetAllAsync()
    {
        var stores = await _storeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<StoreDto>>(stores);
    }

    public async Task<StoreDto?> GetByIdAsync(Guid id)
    {
        var store = await _storeRepository.GetByIdAsync(id);
        return _mapper.Map<Store?, StoreDto>(store);
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

    public void Dispose()
    {
        _storeRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
