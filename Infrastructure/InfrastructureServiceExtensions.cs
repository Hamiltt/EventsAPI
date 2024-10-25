using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Events.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EventsDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
