namespace SureProfit.Domain;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
