using Microsoft.AspNetCore.Mvc;
using SureProfit.Api.Controllers.V1;
using SureProfit.Application;
using SureProfit.Application.Notifications;

namespace SureProfit.Api;

[Route("api/v{version:apiVersion}/[controller]")]
public class StoreController(IStoreService storeService, INotifier notifier) : MainController(notifier)
{
    private readonly IStoreService _storeService = storeService;

    [HttpGet]
    public async Task<IEnumerable<StoreDto>> GetAllAsync()
    {
        return await _storeService.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StoreDto>> GetById(Guid id)
    {
        var store = await _storeService.GetByIdAsync(id);

        if (store is null)
        {
            return NotFound("Store not found");
        }

        return Ok(store);
    }

    [HttpPost]
    public async Task<ActionResult<StoreDto>> Create(StoreDto storeDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        storeDto.Id = await _storeService.CreateAsync(storeDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return CreatedAtAction("GetById", new { id = storeDto.Id }, storeDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, StoreDto storeDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        if (storeDto.Id != id)
        {
            Notify("Route and object ids are not the same");
            return HandleBadRequest();
        }

        await _storeService.UpdateAsync(storeDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _storeService.DeleteAsync(id);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return NoContent();
    }
}
