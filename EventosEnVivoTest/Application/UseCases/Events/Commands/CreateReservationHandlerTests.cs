using Application.Contracts.Persistence;
using Application.UseCases.Reservations.Command;
using Application.UseCases.Reservations.Handlers;
using Domain.Entities;
using Moq;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class CreateReservationHandlerTests
    {
        [Fact]
        public async Task CrearReserva_DeberiaGuardarCorrectamente()
        { // Arrange
            var eventRepoMock = new Mock<IEventRepository>();
            var reservationRepoMock = new Mock<IReservationRepository>();

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
            Assert.NotEqual(Guid.Empty, result); //  Guid válido
            reservationRepoMock.Verify(r => r.AddAsync(It.IsAny<Reservation>()), Times.Once);
        }
    }
}
