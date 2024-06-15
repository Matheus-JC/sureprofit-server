using Microsoft.AspNetCore.Mvc;
using SureProfit.Application.Notifications;
using SureProfit.Application.TagManagement;

namespace SureProfit.Api.Controllers.V1;

[Route(Routes.Base)]
public class TagController(ITagService tagService, INotifier notifier) : MainController(notifier)
{
    private readonly ITagService _tagService = tagService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAllAsync()
    {
        return Ok(await _tagService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TagDto>> GetByIdAsync(Guid id)
    {
        var tag = await _tagService.GetByIdAsync(id);

        if (tag is null)
        {
            return NotFound("Tag not found");
        }

        return tag;
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateAsync(TagDto tagDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        tagDto.Id = await _tagService.CreateAsync(tagDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return CreatedAtAction("GetById", new { id = tagDto.Id }, tagDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, TagDto tagDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        if (id != tagDto.Id)
        {
            Notify("Route and object ids are not the same");
            return HandleBadRequest();
        }

        await _tagService.UpdateAsync(tagDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _tagService.DeteleAsync(id);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return NoContent();
    }
}
