using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class EventCapacityTests
    {
        [Fact]
        public void CrearEvento_ConCapacidadMayorQueVenue_DeberiaFallar()
        {
            // Arrange
            var venue = new Venue("Auditorio Central", 100, "Medellín");

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() =>
            {
                var evento = new Event(
                    "Concierto Prueba",
                    "Descripción del concierto de prueba",
                    200, // capacidad mayor que la del venue
                    DateTime.UtcNow.AddDays(5),
                    DateTime.UtcNow.AddDays(6),
                    "concierto",
                   venue,
                   100000
                );
            });

            Assert.Equal("La capacidad del evento no puede exceder la del venue.", ex.Message);
        }

        [Fact]
        public void CrearEvento_ConCapacidadValida_DeberiaCrearCorrectamente()
        {
            // Arrange
            var venue = new Venue("Sala Norte", 150, "Medellín");

            // Act
            var evento = new Event(
                "Taller QA",
                "Descripción del taller de QA",
                100, // ✅ capacidad dentro del límite
                DateTime.UtcNow.AddDays(2),
                DateTime.UtcNow.AddDays(3),
                "taller",
                venue,
                50000
            );

            // Assert
            Assert.Equal(100, evento.Capacity);
            Assert.Equal(venue.Id, evento.VenueId);
        }
    }
}
           