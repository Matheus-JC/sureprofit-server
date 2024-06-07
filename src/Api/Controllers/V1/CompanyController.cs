using Microsoft.AspNetCore.Mvc;
using SureProfit.Application;
using SureProfit.Application.Notifications;

namespace SureProfit.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
public class CompanyController(ICompanyService companyService, INotifier notifier) : MainController(notifier)
{
    private readonly ICompanyService _companyService = companyService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllAsync()
    {
        return Ok(await _companyService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyDto>> GetByIdAsync(Guid id)
    {
        var company = await _companyService.GetByIdAsync(id);

        if (company is null)
        {
            return NotFound("Company not found");
        }

        return company;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CompanyDto companyDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        companyDto.Id = await _companyService.CreateAsync(companyDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return CreatedAtAction("GetById", new { id = companyDto.Id }, companyDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, CompanyDto companyDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        if (companyDto.Id != id)
        {
            Notify("Route and object ids are not the same");
            return HandleBadRequest();
        }

        await _companyService.UpdateAsync(companyDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _companyService.DeleteAsync(id);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return NoContent();
    }
}
