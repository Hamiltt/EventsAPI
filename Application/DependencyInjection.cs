using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<IAuthService, AuthService>();

        // Регистрация репозиториев
        //services.AddScoped<IEventRepository, EventRepository>();
        //services.AddScoped<IParticipantRepository, ParticipantRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
    }
}