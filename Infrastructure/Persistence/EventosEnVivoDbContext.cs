using Domain.Entities;
using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class EventosEnVivoDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public EventosEnVivoDbContext(DbContextOptions<EventosEnVivoDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventosEnVivoDbContext).Assembly);
            // Relación Event -> Reservations
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Reservations)
                .WithOne()
                .HasForeignKey(r => r.EventId);

            // Relación Venue -> Events
            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Events)
                .WithOne()
                .HasForeignKey(e => e.VenueId);
            // Aplicar datos iniciales
            modelBuilder.ApplySeedData();
        }
    }
}
