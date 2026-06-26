using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SeedData
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class ApplySeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venue>().HasData(
                new Venue
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Auditorio Central",
                    Capacity = 200,
                    Location = "Bogotá"
                },
                new Venue
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Sala Norte",
                    Capacity = 50,
                    Location = "Bogotá"
                },
                new Venue
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Arena Sur",
                    Capacity = 500,
                    Location = "Medellín"
                }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Conferencia de Tecnología",
                    Description = "Descripción de la conferencia de tecnología",
                    // Fecha fija: 25 de Julio de 2026, de 2:00 PM a 4:00 PM UTC
                    StartDate = new DateTime(2026, 7, 25, 14, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 7, 25, 16, 0, 0, DateTimeKind.Utc),
                    Capacity = 200,
                    Type = "conferencia",
                    Price = 200,
                    VenueId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Event
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Title = "Taller de Fotografía",
                    Description = "Descripción del taller de fotografía",
                    // Fecha fija: 10 de Agosto de 2026, de 9:00 AM a 11:00 AM UTC
                    StartDate = new DateTime(2026, 8, 10, 9, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 8, 10, 11, 0, 0, DateTimeKind.Utc),
                    Capacity = 50,
                    Type = "taller",
                    Price = 100,
                    VenueId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Event
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Title = "Concierto de Rock",
                    Description = "Descripción del concierto de rock",
                    // Fecha fija: 25 de Agosto de 2026, de 8:00 PM a 10:00 PM UTC
                    StartDate = new DateTime(2026, 8, 25, 20, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 8, 25, 22, 0, 0, DateTimeKind.Utc),
                    Capacity = 500,
                    Type = "concierto",
                    Price = 500,
                    VenueId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                }
            );
        }
    }

}
