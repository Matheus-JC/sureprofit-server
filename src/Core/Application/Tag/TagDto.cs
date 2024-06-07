namespace SureProfit.Application;

public class TagDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; } = true;
}
