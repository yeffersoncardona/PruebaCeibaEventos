using Application.Contracts.Persistence;
using Application.UseCases.Payments.Commands;
using Application.UseCases.Payments.Handlers;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace EventosEnVivoTest.Application.UseCases.Reservations.Commands
{
    public class ConfirmReservationHandlerTests
    {
        [Fact]
        public async Task ConfirmarPago_DeberiaCambiarEstadoReservaAConfirmed()
        {
            // Arrange
            var reserva = Reservation.Create(Guid.NewGuid(), "Yefferson", "yefferson@test.com", 2);

            var reservationRepoMock = new Mock<IReservationRepository>();
            reservationRepoMock.Setup(r => r.GetByIdAsync(reserva.Id))
                               .ReturnsAsync(reserva);
            reservationRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Reservation>()))
                               .Returns(Task.CompletedTask);

            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(p => p.AddAsync(It.IsAny<Domain.Entities.Payment>()))
                           .Returns(Task.CompletedTask);

            var handler = new ConfirmPaymentHandler(reservationRepoMock.Object, paymentRepoMock.Object);

            var command = new ConfirmPaymentCommand
            {
                ReservationId = reserva.Id,
                Amount = 100m
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // devuelve el Id de la reserva confirmada
            Assert.Equal(ReservationStatus.Confirmed, reserva.Status); // RN‑05
            Assert.NotNull(reserva.ReservationCode); // se asigna código único
            paymentRepoMock.Verify(p => p.AddAsync(It.IsAny<Domain.Entities.Payment>()), Times.Once);
            reservationRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Reservation>()), Times.Once);
        }
    }
}
