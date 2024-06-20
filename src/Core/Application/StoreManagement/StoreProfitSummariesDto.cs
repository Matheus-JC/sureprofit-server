namespace SureProfit.Application.StoreManagement;

public class StoreProfitSummariesDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal? GrossProfit { get; set; }
}
