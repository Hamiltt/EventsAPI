using Domain.Repositories;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        IParticipantRepository Participants { get; }
        IUserRepository Users { get; }

        Task CompleteAsync();
    }
}