using Microsoft.AspNetCore.Mvc;
using SureProfit.Api.Controllers.V1;
using SureProfit.Application;
using SureProfit.Application.CostManagement;
using SureProfit.Application.Notifications;

namespace SureProfit.Api;

[Route(Routes.Base)]
public class CostController(ICostService costService, INotifier notifier) : MainController(notifier)
{
    private readonly ICostService _costService = costService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostDto>>> GetAllAsync()
    {
        return Ok(await _costService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CostDto>> GetByIdAsync(Guid id)
    {
        var costDto = await _costService.GetByIdAsync(id);

        if (costDto is null)
        {
            return NotFound("Cost not found");
        }

        return Ok(costDto);
    }

    [HttpPost]
    public async Task<ActionResult<CostDto>> CreateAsync(CostDto costDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        costDto.Id = await _costService.CreateAsync(costDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return CreatedAtAction("GetById", new { id = costDto.Id }, costDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id, CostDto costDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        if (costDto.Id != id)
        {
            Notify("Route and object ids are not the same");
            return HandleBadRequest();
        }

        await _costService.UpdateAsync(costDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _costService.DeleteAsync(id);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return NoContent();
    }
}
