using Application.Contracts.Persistence;
using Application.UseCases.Reservations.Command;
using Application.UseCases.Reservations.Handlers;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class CancelReservationHandlerTests
    {
        [Fact]
        public async Task CancelarReserva_DeberiaCambiarEstadoA_Cancelled()
        {
            var options = new DbContextOptionsBuilder<EventosEnVivoDbContext>()
        .UseInMemoryDatabase("CancelReservationTestDB")
        .Options;

            using var context = new EventosEnVivoDbContext(options);

            // Crear evento
            var evento = new Event(
                "Concierto de prueba",
                "Descripción del concierto de prueba",  
                100,
               DateTime.UtcNow.AddDays(3),   //  más de 48 horas
               DateTime.UtcNow.AddDays(4),
                "concierto",
                new Venue("Auditorio Central", 100, "Medellín"),
                125000

            );
               

            // Crear reserva confirmada
            var reserva = Reservation.Create(evento.Id, "Yefferson", "yefferson@test.com", 2);
            reserva.ConfirmPayment();

            // Guardar evento y reserva en la BD InMemory
            await context.Events.AddAsync(evento);
            await context.Reservations.AddAsync(reserva);
            await context.SaveChangesAsync();

            var eventRepo = new EventRepository(context);
            var reservationRepo = new ReservationRepository(context);

            var handler = new CancelReservationHandler( reservationRepo, eventRepo);

            var command = new CancelReservationCommand { ReservationId = reserva.Id };
            // Act
            await handler.Handle(command, default);

            // Assert con repositorio real
            var reservaDb = await context.Reservations.FindAsync(reserva.Id);
            Assert.Equal(ReservationStatus.Cancelled, reservaDb.Status);

        }
        [Fact]
        public async Task CancelarReserva_DeberiaCambiarEstadoA_Lost_SiEventoEsMenosDe48Horas()
        {
            var options = new DbContextOptionsBuilder<EventosEnVivoDbContext>()
                .UseInMemoryDatabase("CancelReservationLostTestDB")
                .Options;

            using var context = new EventosEnVivoDbContext(options);

            // Crear evento que empieza en 24h (<48h)
            var evento = new Event(
                "Concierto penalizado",
                "Descripción del concierto penalizado",
                100,
                DateTime.UtcNow.AddHours(24),
                DateTime.UtcNow.AddHours(26),
                "concierto",
                new Venue("Auditorio Central", 100, "Medellín"),
                350000
            );

            // Crear reserva confirmada
            var reserva = Reservation.Create(evento.Id, "Yefferson", "yefferson@test.com", 2);
            reserva.ConfirmPayment();

            await context.Events.AddAsync(evento);
            await context.Reservations.AddAsync(reserva);
            await context.SaveChangesAsync();

            var eventRepo = new EventRepository(context);
            var reservationRepo = new ReservationRepository(context);

            var handler = new CancelReservationHandler(reservationRepo, eventRepo);
            var command = new CancelReservationCommand { ReservationId = reserva.Id };

            // Act
            await handler.Handle(command, default);

            // Assert
            var reservaDb = await context.Reservations.FindAsync(reserva.Id);
            Assert.Equal(ReservationStatus.Lost, reservaDb.Status); // RN‑07
        }
    }
}
