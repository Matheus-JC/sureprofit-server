using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SureProfit.Api.Models;
using SureProfit.Application.CostManagement;
using SureProfit.Application.Notifications;
using SureProfit.Application.StoreManagement;

namespace SureProfit.Api.Controllers.V1;

[Authorize]
[Route(Routes.Base)]
public class StoreController(IStoreService storeService, INotifier notifier) : MainController(notifier)
{
    private readonly IStoreService _storeService = storeService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoreDto>>> GetAllAsync()
    {
        return Ok(await _storeService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StoreDto>> GetByIdAsync(Guid id)
    {
        var storeDto = await _storeService.GetByIdAsync(id);

        if (storeDto is null)
        {
            return NotFound("Store not found");
        }

        return Ok(storeDto);
    }

    [HttpGet("{storeId:guid}/cost")]
    public async Task<ActionResult<IEnumerable<CostDto>>> GetVariableCostsByStoreAsync(Guid storeId)
    {
        return Ok(await _storeService.GetVariableCostsByStoreAsync(storeId));
    }

    [HttpGet("gross-profit")]
    public async Task<ActionResult<IEnumerable<StoreProfitSummariesDto>>> GetStoresProfitSumariesAsync()
    {
        return Ok(await _storeService.GetStoresProfitSumariesAsync());
    }

    [HttpPost]
    public async Task<ActionResult<StoreDto>> CreateAsync(StoreDto storeDto)
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
    public async Task<ActionResult> UpdateAsync(Guid id, StoreDto storeDto)
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
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _storeService.DeleteAsync(id);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return NoContent();
    }

    [HttpGet("{storeId:guid}/markup-multiplier")]
    public async Task<ActionResult<decimal>> CalculateMarkupMultiplierAsync(Guid storeId)
    {
        var markupMultiplier = await _storeService.CalculateMarkupMultiplier(storeId);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok(markupMultiplier);
    }
}
