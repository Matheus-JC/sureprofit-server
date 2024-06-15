
using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application.CostManagement;

public class CostService(
    ICostRepository costRepository, IStoreRepository storeRepository, ITagRepository tagRepository,
    IUnitOfWork unitOfWork, INotifier notifier, IMapper mapper
)
    : BaseService(unitOfWork, notifier, mapper), ICostService
{
    private readonly ICostRepository _costRepository = costRepository;
    private readonly IStoreRepository _storeRepository = storeRepository;
    private readonly ITagRepository _tagRepository = tagRepository;

    public async Task<IEnumerable<CostDto>> GetAllAsync()
    {
        var costs = await _costRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CostDto>>(costs);
    }

    public async Task<CostDto?> GetByIdAsync(Guid id)
    {
        var cost = await _costRepository.GetByIdAsync(id);
        return _mapper.Map<CostDto>(cost);
    }

    public async Task<Guid> CreateAsync(CostDto costDto)
    {
        var validator = new CostDtoValidator(_costRepository, _storeRepository, _tagRepository, validateId: false);
        if (!await Validate(validator, costDto))
        {
            return Guid.Empty;
        }

        costDto.Id = Guid.Empty;
        var cost = _mapper.Map<Cost>(costDto);

        _costRepository.Create(cost);

        await CommitAsync();

        return cost.Id;
    }

    public async Task UpdateAsync(CostDto costDto)
    {
        var validator = new CostDtoValidator(_costRepository, _storeRepository, _tagRepository, validateId: false);
        if (!await Validate(validator, costDto))
        {
            return;
        }

        var cost = _mapper.Map<Cost>(costDto);

        _costRepository.Update(cost);

        await CommitAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var cost = await _costRepository.GetByIdAsync(id);

        if (cost is null)
        {
            Notify("Cost not found");
            return;
        }

        _costRepository.Delete(cost);

        await CommitAsync();
    }

    public void Dispose()
    {
        _costRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
