using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosEnVivoTest.Application.UseCases.Events.Commands
{
    public class EventScheduleConflictTests
    {
        [Fact]
        public void CrearEvento_ConConflictoDeHorarios_DeberiaFallar()
        {
            // Arrange
            var venue = new Venue("Sala Norte", 100, "Medellín");

            var eventoExistente = new Event(
                "Evento 1",
                "Descripción del evento 1",
                80,
                new DateTime(2026, 7, 10, 10, 0, 0), // 10:00 AM
                new DateTime(2026, 7, 10, 12, 0, 0), // 12:00 PM
                "conferencia",
                venue,
                100000
            );

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() =>
            {
                var eventoConflicto = new Event(
                    "Evento 2",
                    "Descripción del evento 2",
                    50,
                    new DateTime(2026, 7, 10, 11, 0, 0), // 11:00 AM
                    new DateTime(2026, 7, 10, 13, 0, 0), // 1:00 PM
                    "taller",
                    venue,
                    500000
                );

                // Aquí deberías tener lógica de dominio que valide conflicto
                eventoExistente.ValidateScheduleConflict(eventoConflicto);
            });

            Assert.Equal("Ya existe un evento en este venue con horarios que se solapan.", ex.Message);
        }

        [Fact]
        public void CrearEvento_SinConflictoDeHorarios_DeberiaCrearCorrectamente()
        {
            // Arrange
            var venue = new Venue("Sala Norte", 100, "Medellín");

            var eventoExistente = new Event(
                "Evento 1",
                "Descripción del evento 1",
                80,
                new DateTime(2026, 7, 10, 10, 0, 0),
                new DateTime(2026, 7, 10, 12, 0, 0),
                "conferencia",
                venue,
                100000
            );

            // Act
            var eventoNuevo = new Event(
                "Evento 2",
                "Descripción del evento 2",
                50,
                new DateTime(2026, 7, 10, 13, 0, 0), // empieza después
                new DateTime(2026, 7, 10, 15, 0, 0),
                "taller",
                venue,
                500000
            );

            // Assert
            Assert.Equal(50, eventoNuevo.Capacity);
            Assert.Equal(venue.Id, eventoNuevo.VenueId);
        }
    }
}
