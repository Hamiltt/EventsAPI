namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}