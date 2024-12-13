using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventsDbContext _context;

        public UnitOfWork(EventsDbContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
            Participants = new ParticipantRepository(_context);
            Users = new UserRepository(_context);
        }

        public IEventRepository Events { get; private set; }
        public IParticipantRepository Participants { get; private set; }
        public IUserRepository Users { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}