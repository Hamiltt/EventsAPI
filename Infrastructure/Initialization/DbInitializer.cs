using Infrastructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Initialization
{
    public static class DbInitializer
    {
        public static void Initialize(EventsDbContext context)
        {
            context.Database.Migrate();

            SeedData(context);
        }

        private static void SeedData(EventsDbContext context)
        {
            if (!context.Events.Any())
            {
                context.Events.AddRange(new List<Event>
            {
                new("Event 1", 100) { Date = DateTime.UtcNow },
                new("Event 2", 200) { Date = DateTime.UtcNow.AddDays(1) }
            });

                context.SaveChanges();
            }
        }
    }
}