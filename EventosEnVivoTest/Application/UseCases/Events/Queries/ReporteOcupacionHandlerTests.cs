using Application.UseCases.Events.Queries;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventosEnVivoTest.Application.UseCases.Events.Queries
{
    public class ReporteOcupacionHandlerTests
    {
        [Fact]
        public async Task ReporteOcupacion_DeberiaCalcularCorrectamenteConEFInMemory()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventosEnVivoDbContext>()
                .UseInMemoryDatabase(databaseName: "ReporteOcupacionTestDB")
                .Options;

            using var context = new EventosEnVivoDbContext(options);

            // Creamos un evento
            var evento = new Event(
                "Concierto de prueba",
                "Descripción del concierto de prueba",
                100,
                DateTime.UtcNow.AddDays(1),   // StartDate
                DateTime.UtcNow.AddDays(2),   // EndDate
                "concierto",                   // Tipo de evento
                new Venue("Auditorio Central", 100, "Medellín"),
                50000
            );

            // Crear reservas
            var reserva1 = Reservation.Create(evento.Id, "Yefferson", "yefferson@test.com", 40);
            reserva1.ConfirmPayment(); //  cambia estado a Confirmed

            var reserva2 = Reservation.Create(evento.Id, "Ana", "ana@test.com", 10);
            reserva2.ConfirmPayment(); //  cambia estado a Confirmed

            // Agregar reservas al evento
            evento.AddReservation(reserva1);
            evento.AddReservation(reserva2);

            // Guardamos en la BD InMemory
            await context.Events.AddAsync(evento);
            await context.Reservations.AddRangeAsync(evento.Reservations);
            await context.SaveChangesAsync();

            // Creamos el handler con el contexto real
            var eventRepo = new EventRepository(context);
            var reservationRepo = new ReservationRepository(context);

            var handler = new ReporteOcupacionHandler(eventRepo, reservationRepo);
            var query = new ReporteOcupacionQuery { EventId = evento.Id };

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Equal(50, result.PorcentajeOcupacion);   // 50% ocupación
            Assert.Equal(50, result.TotalVendidas);         // 40 + 10
            Assert.Equal(50, result.TotalDisponibles);      // 100 - 50
        }
    }
}
