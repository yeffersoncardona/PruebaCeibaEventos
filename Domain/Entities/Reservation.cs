using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerEmail { get; private set; }
        public int Quantity { get; private set; }
        public ReservationStatus Status { get; private set; }
        public string? ReservationCode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        public void ConfirmPayment()
        {
            if (Status == ReservationStatus.Confirmed)
                throw new DomainException("Reserva ya confirmada");
            if (Status == ReservationStatus.Cancelled)
                throw new DomainException("Reserva cancelada, no se puede confirmar");

            Status = ReservationStatus.Confirmed;
            ReservationCode = $"EV-{Random.Shared.Next(100000, 999999)}"; // .NET 10: Random.Shared
        }
        // Constructor parametrizado
        private Reservation(Guid eventId, string buyerName, string buyerEmail, int quantity)
        {
            Id = Guid.NewGuid();
            EventId = eventId;
            BuyerName = buyerName;
            BuyerEmail = buyerEmail;
            Quantity = quantity;
            Status = ReservationStatus.PendingPayment;
            CreatedAt = DateTime.UtcNow;
        }

        // Constructor vacío requerido por EF Core
        private Reservation()
        {
        }
        // Método de dominio → Cancelar reserva
        public void Cancel()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new DomainException("Solo reservas confirmadas pueden cancelarse.");

            Status = ReservationStatus.Cancelled;
            CancelledAt = DateTime.UtcNow; // registrar fecha/hora
                                           // liberar entradas → lógica en Event o repositorio
        }

        public void MarkAsLost()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new DomainException("Solo reservas confirmadas pueden marcarse como perdidas.");

            Status = ReservationStatus.Lost;
            CancelledAt = DateTime.UtcNow; // registrar fecha/hora
                                           // no liberar entradas
        }
        public static Reservation Create(Guid eventId, string buyerName, string buyerEmail, int quantity)
        {
            return new Reservation(eventId, buyerName, buyerEmail, quantity);
        }

    }
}
