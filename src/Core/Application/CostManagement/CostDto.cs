using SureProfit.Domain;

namespace SureProfit.Application.CostManagement;

public class CostDto
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    public Guid? TagId { get; set; }
    public required string Description { get; set; } = string.Empty;
    public required decimal Value { get; set; }
    public CostType Type { get; set; }

}
