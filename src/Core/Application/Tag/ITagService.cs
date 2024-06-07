namespace SureProfit.Application;

public interface ITagService : IDisposable
{
    Task<IEnumerable<TagDto>> GetAllAsync();
    Task<TagDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(TagDto tagDto);
    Task UpdateAsync(TagDto tagDto);
    Task DeteleAsync(Guid id);
}
