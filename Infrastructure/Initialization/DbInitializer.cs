using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Initialization
{
    public static class DbInitializer
    {
        public static void Initialize(EventsDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Events.Any())
            {
                return; // База данных уже заполнена
            }

            var events = new Event[]
            {
                new() { Category = "ITEvent", Location = "Vitebsk" },
                new() { Category = ".NET Conf", Location = "Minsk" }
            };

            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}