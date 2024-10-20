using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;

namespace Events.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Регистрация контекста базы данных с использованием Entity Framework Core
            services.AddDbContext<EventsDbContext>(options =>
                options.UseSqlServer(connectionString)); // Используйте UseNpgsql для PostgreSQL

            // Регистрация репозиториев
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();

            // Регистрация UnitOfWork, если используете его
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
