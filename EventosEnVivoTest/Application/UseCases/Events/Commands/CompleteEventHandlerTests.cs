using Application.Contracts.Persistence;
using Application.UseCases.Events.Handlers;
using Domain.Entities;
using Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class CompleteEventHandlerTests
    {
        [Fact]
        public async Task CompletarEvento_DeberiaCambiarEstadoACompleted_SiEndDateEsPasado()
        {
            // Arrange
            var venue = new Venue("Auditorio Central", 200, "Medellín");
            var evento = new Event("Concierto Rock", "Descripción del concierto KISS", 200, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1), "concierto", venue, 400000);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(r => r.GetByIdAsync(evento.Id))
                         .ReturnsAsync(evento);
            eventRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Event>()))
                         .Returns(Task.CompletedTask);

            var handler = new CompleteEventHandler(eventRepoMock.Object);

            var command = new CompleteEventCommand
            {
                EventId = evento.Id
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // devuelve el Id del evento completado
            Assert.Equal(EventStatus.Completed, evento.Status);  // RN‑06
            eventRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Event>()), Times.Once);
        }
        [Fact]
        public async Task CompletarEvento_NoDebeCambiarEstado_SiEndDateEsFuturo()
        {
            // Arrange
            var venue = new Venue("Auditorio Central", 200, "Medellín");
            var evento = new Event("Concierto Futuro", "Descripción del concierto futuro", 200, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), "concierto", venue, 400000);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(r => r.GetByIdAsync(evento.Id))
                         .ReturnsAsync(evento);

            var handler = new CompleteEventHandler(eventRepoMock.Object);

            var command = new CompleteEventCommand
            {
                EventId = evento.Id
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.NotEqual(EventStatus.Completed, evento.Status); // sigue en estado inicial
            eventRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Event>()), Times.Never);
        }
    }
}
