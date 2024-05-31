using System.Net;
using Microsoft.AspNetCore.Mvc;
using SureProfit.Application;
using SureProfit.Application.Notifications;

namespace SureProfit.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
public class CompanyController(ICompanyService companyService, INotifier notifier) : MainController(notifier)
{
    private readonly ICompanyService _companyService = companyService;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyDto>> GetById(Guid id)
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
            return HandleBadRequest(ModelState);

        var Id = await _companyService.CreateAsync(companyDto);

        if (IsOperationInvalid() || Id is null)
        {
            return HandleBadRequest();
        }

        companyDto.Id = Id;

        return CreatedAtAction("GetById", new { id = Id }, companyDto);
    }
}
