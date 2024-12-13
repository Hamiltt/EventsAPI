using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event("Event 1", 100)
                {
                    Id = 1,
                    Description = "Description for Event 1",
                    Date = DateTime.UtcNow.AddDays(1),
                    Location = "Location 1",
                    Category = "Category 1"
                },
                new Event("Event 2", 200)
                {
                    Id = 2,
                    Description = "Description for Event 2",
                    Date = DateTime.UtcNow.AddDays(2),
                    Location = "Location 2",
                    Category = "Category 2"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "982137182937128",
                    Email = "admin@example.com"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    PasswordHash = "451326347892323876",
                    Email = "user@example.com"
                }
            );

            modelBuilder.Entity<Participant>().HasData(
                new Participant("John", 1)
                {
                    Id = 1,
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    RegistrationDate = DateTime.UtcNow,
                    Email = "john.doe@example.com"
                },
                new Participant("Jane", 2)
                {
                    Id = 2,
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1995, 5, 5),
                    RegistrationDate = DateTime.UtcNow,
                    Email = "jane.smith@example.com"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}