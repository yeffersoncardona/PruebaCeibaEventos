namespace Domain.Enums
{
    public enum ReservationStatus
    {
        PendingPayment, // Reserva creada, esperando confirmación de pago
        Confirmed,      // Pago verificado, reserva activa
        Cancelled,       // Reserva cancelada
        Lost             // Reserva perdida (RN‑07)
    }
}
