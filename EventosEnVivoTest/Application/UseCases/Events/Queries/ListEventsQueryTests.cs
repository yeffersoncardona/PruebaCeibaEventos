using Application.Contracts.Persistence;
using Application.UseCases.Events.Queries;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Application.UseCases.Events.Queries
{
    public class ListEventsQueryTests
    {
        [Fact]
        public async Task ListEvents_DeberiaRetornarEventosCorrectos()
        {
            // Arrange
            var eventos = new List<Event>
        {
            new Event("Concierto Rock", "Descripción del concierto de rock", 200, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), "concierto", new Venue("Auditorio Central", 200, "Medellín"), 500000),
            new Event("Obra de Teatro", "Descripción de la obra de teatro", 150, DateTime.UtcNow.AddDays(3), DateTime.UtcNow.AddDays(4), "teatro", new Venue("Sala Norte", 150, "Medellín"), 190000)
        };

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(r => r.GetAllAsync())
                         .ReturnsAsync(eventos);

            var handler = new ListEventsQueryHandler(eventRepoMock.Object);
            var query = new ListEventsQuery();

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal("Concierto Rock", result[0].Title);
            Assert.Equal(200, result[0].Capacity);

            Assert.Equal("Obra de Teatro", result[1].Title);
            Assert.Equal(150, result[1].Capacity);
        }
    }
}
