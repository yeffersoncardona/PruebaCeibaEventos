using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Infraestructure.Persistence
{
    public class EventosEnVivoDbContextTests
    {
        [Fact]
        public async Task GuardarYRecuperarReserva_DeberiaFuncionarConInMemory()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventosEnVivoDbContext>()
                .UseInMemoryDatabase(databaseName: "EventosDb")
                .Options;

            using var context = new EventosEnVivoDbContext(options);

            var reservation = Reservation.Create(
                Guid.NewGuid(),
                "Yefferson",
                "yefferson@test.com",
                2
            );

            // Act
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();

            var saved = await context.Reservations.FirstOrDefaultAsync(r => r.Id == reservation.Id);

            // Assert
            Assert.NotNull(saved);
            Assert.Equal("Yefferson", saved.BuyerName);
            Assert.Equal(2, saved.Quantity);
        }
    }
}
