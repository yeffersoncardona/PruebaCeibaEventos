using Application.Contracts.Persistence;
using Application.UseCases.Reservations.Commands;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class DeleteReservationHandlerTests
    {
        [Fact]
        public async Task EliminarReserva_DeberiaRemoverDelRepositorio()
        {
            // Arrange
            var reserva = Reservation.Create(Guid.NewGuid(), "Yefferson", "yefferson@test.com", 2);

            var reservationRepoMock = new Mock<IReservationRepository>();
            reservationRepoMock.Setup(r => r.GetByIdAsync(reserva.Id))
                               .ReturnsAsync(reserva);
            reservationRepoMock.Setup(r => r.DeleteAsync(reserva.Id))
                   .Returns(Task.CompletedTask);

            var handler = new DeleteReservationHandler(reservationRepoMock.Object);

            var command = new DeleteReservationCommand
            {
                ReservationId = reserva.Id
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // ✅ ahora compila
            reservationRepoMock.Verify(r => r.DeleteAsync(reserva.Id), Times.Once);
        }
    }
}
