using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public class Event
    {
        private EventStatus status = EventStatus.Active;

        public Guid Id { get;  set; }
        public string Title { get;  set; } = string.Empty; // 5-100 caracteres
        public string Description { get; set; } = string.Empty; // 10-500 caracteres
        public string Type { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public Guid VenueId { get;  set; }
        public EventStatus Status { get => status; set => status = value; }
        public int Capacity { get;  set; }
        public decimal Price { get;  set; }
        public List<Reservation> Reservations { get; private set; } = new();

        public Event() { }
        public Event(string title, string description, int capacity, DateTime startDate, DateTime endDate, string type, Venue venue ,decimal price)
        {
            //RN‑01: Validar capacidad
            if (capacity > venue.Capacity)
                throw new DomainException("La capacidad del evento no puede exceder la del venue.");

            // RN‑03: Validar fin de semana
            if ((startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
                && startDate.Hour >= 22)
            {
                throw new DomainException("Los eventos en fin de semana no pueden iniciar después de las 22:00.");
            }
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Capacity = capacity;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            Price = price;
            VenueId = venue.Id;
        }

        // Método para agregar reservas
        public void AddReservation(Reservation reservation)
        {
            Reservations.Add(reservation);
        }

        public void MarkCompletedIfFinished()
        {
            if (DateTime.UtcNow > EndDate)
                Status = EventStatus.Completed; // RN-06
        }

        public void ValidateBusinessRules(IEnumerable<Event> existingEvents, Venue venue)
        {
            if (Capacity > venue.Capacity)
                throw new DomainException("Capacidad excede la del venue"); // RN-01

            if (existingEvents.Any(e => e.VenueId == VenueId &&
                                        e.Status == EventStatus.Active &&
                                        e.StartDate < EndDate &&
                                        e.EndDate > StartDate))
                throw new DomainException("Conflicto de horarios en el venue"); // RN-02

            if ((StartDate.DayOfWeek == DayOfWeek.Saturday || StartDate.DayOfWeek == DayOfWeek.Sunday) &&
                StartDate.Hour > 22)
                throw new DomainException("Eventos en fin de semana no pueden iniciar después de las 22:00"); // RN-03
        }
        public void MarkAsCompleted()
        {
            if (EndDate < DateTime.UtcNow)
            {
                Status = EventStatus.Completed;
            }
        }
        public void ValidateScheduleConflict(Event otherEvent)
        {
            if (VenueId == otherEvent.VenueId &&
                StartDate < otherEvent.EndDate &&
                EndDate > otherEvent.StartDate)
            {
                throw new DomainException("Ya existe un evento en este venue con horarios que se solapan.");
            }
        }

    }
}
