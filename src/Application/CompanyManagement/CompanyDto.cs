namespace SureProfit.Application.CompanyManagement;

public class CompanyDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Cnpj { get; set; }
}
