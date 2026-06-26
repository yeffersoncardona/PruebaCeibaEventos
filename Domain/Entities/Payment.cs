using Domain.Enums;

namespace Domain.Entities
{
    public class Payment
    {

        public Guid Id { get; private set; }
        public Guid ReservationId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime? PaidAt { get; private set; }

        public void Confirm(decimal amount)
        {
            Amount = amount;
            Status = PaymentStatus.Paid;
            PaidAt = DateTime.UtcNow;
        }
        // Constructor parametrizado
        public Payment(Guid id, Guid reservationId, decimal amount, PaymentStatus status, DateTime paidAt)
        {
            Id = id;
            ReservationId = reservationId;
            Amount = amount;
            Status = status;
            PaidAt = paidAt;
        }

        // Constructor vacío requerido por EF Core
        private Payment() { }
    }
}
