namespace SureProfit.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
