using SureProfit.Domain.Enums;

namespace SureProfit.Application.StoreManagement;

public class StoreDto
{
    public Guid Id { get; set; }
    public required Guid CompanyId { get; set; }
    public required string Name { get; set; }
    public required StoreEnviroment Enviroment { get; set; }
    public decimal? TargetProfit { get; set; }
    public decimal PerItemFee { get; set; }
}
