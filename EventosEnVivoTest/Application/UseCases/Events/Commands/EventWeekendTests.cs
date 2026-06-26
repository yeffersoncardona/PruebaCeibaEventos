using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class EventWeekendTests
    {
        [Fact]
        public void CrearEvento_FinDeSemanaDespuesDe22_DeberiaFallar()
        {
            // Arrange
            var venue = new Venue("Arena Sur", 500, "Medellín");

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() =>
            {
                var evento = new Event(
                    "Concierto nocturno",
                    "Descripción del concierto nocturno",
                    300,
                    new DateTime(2026, 7, 11, 23, 0, 0), // sábado 11 PM
                    new DateTime(2026, 7, 12, 1, 0, 0),
                    "concierto",
                    venue,
                    600000
                );
            });

            Assert.Equal("Los eventos en fin de semana no pueden iniciar después de las 22:00.", ex.Message);
        }

        [Fact]
        public void CrearEvento_FinDeSemanaAntesDe22_DeberiaCrearCorrectamente()
        {
            var venue = new Venue("Arena Sur", 500, "Medellín");

            var evento = new Event(
                "Concierto permitido",
                "Descripción del concierto permitido",
                300,
                new DateTime(2026, 7, 11, 20, 0, 0), // sábado 8 PM
                new DateTime(2026, 7, 11, 22, 0, 0),
                "concierto",
                venue,
                600000
            );

            Assert.Equal(300, evento.Capacity);
            Assert.Equal(venue.Id, evento.VenueId);
            Assert.Equal("Concierto permitido", evento.Title);
        }
    }
}
