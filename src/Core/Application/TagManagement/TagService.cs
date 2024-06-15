
using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain;
using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces.Data;

namespace SureProfit.Application.TagManagement;

public class TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, INotifier notifier, IMapper mapper)
    : BaseService(unitOfWork, notifier, mapper), ITagService
{
    private readonly ITagRepository _tagRepository = tagRepository;

    public async Task<IEnumerable<TagDto>> GetAllAsync()
    {
        var tags = await _tagRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<TagDto?> GetByIdAsync(Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        return _mapper.Map<TagDto>(tag);
    }

    public async Task<Guid> CreateAsync(TagDto tagDto)
    {
        if (!await Validate(new TagDtoValidator(_tagRepository, validateId: false), tagDto))
        {
            return Guid.Empty;
        }

        tagDto.Id = Guid.Empty;
        var tag = _mapper.Map<Tag>(tagDto);

        _tagRepository.Create(tag);

        await CommitAsync();

        return tag.Id;
    }

    public async Task UpdateAsync(TagDto tagDto)
    {
        if (!await Validate(new TagDtoValidator(_tagRepository, validateId: true), tagDto))
        {
            return;
        }

        var tag = _mapper.Map<Tag>(tagDto);

        _tagRepository.Update(tag);

        await CommitAsync();
    }

    public async Task DeteleAsync(Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag is null)
        {
            Notify("Tag not found");
            return;
        }

        _tagRepository.Delete(tag);

        await CommitAsync();
    }

    public void Dispose()
    {
        _tagRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
