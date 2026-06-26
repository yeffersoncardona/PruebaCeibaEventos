using Application.Contracts.Persistence;
using Application.UseCases.Reservations.Command;
using Application.UseCases.Reservations.Handlers;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace EventosEnVivoTest.Application.UseCases.Reservations.Commands
{
    public class CreateReservationHandlerTests
    {
        [Fact]
        public async Task CrearReserva_DeberiaGuardarConEstadoPendingPayment()
        {
            // Arrange
            var eventRepoMock = new Mock<IEventRepository>();
            var reservationRepoMock = new Mock<IReservationRepository>();

            Reservation? reservaGuardada = null;
            reservationRepoMock.Setup(r => r.AddAsync(It.IsAny<Reservation>()))
                               .Callback<Reservation>(r => reservaGuardada = r)
                               .Returns(Task.CompletedTask);

            var handler = new CreateReservationHandler(eventRepoMock.Object, reservationRepoMock.Object);

            var command = new CreateReservationCommand
            {
                EventId = Guid.NewGuid(),
                BuyerName = "Yefferson",
                BuyerEmail = "yefferson@test.com",
                Quantity = 2
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // devuelve el Id
            Assert.NotNull(reservaGuardada);
            Assert.Equal(ReservationStatus.PendingPayment, reservaGuardada.Status); // RN‑04
        }
    }
}
