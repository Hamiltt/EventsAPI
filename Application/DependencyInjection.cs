using Application.UseCases.Auth;
using Application.UseCases.Events;
using Application.UseCases.Participants;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<LoginUseCase>();
            services.AddScoped<RefreshTokenUseCase>();

            services.AddScoped<CreateEventUseCase>();
            services.AddScoped<DeleteEventUseCase>();
            services.AddScoped<GetEventByIdUseCase>();
            services.AddScoped<UpdateEventUseCase>();
            services.AddScoped<GetAllEventsUseCase>();
            services.AddScoped<GetEventsByNameUseCase>();

            services.AddScoped<GetParticipantByIdUseCase>();
            services.AddScoped<GetParticipantsByEventUseCase>();
            services.AddScoped<RegisterParticipantUseCase>();
            services.AddScoped<UnregisterParticipantUseCase>();
        }
    }
}