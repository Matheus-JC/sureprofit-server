namespace SureProfit.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
