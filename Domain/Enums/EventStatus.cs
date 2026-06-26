namespace Domain.Enums
{
    public enum EventStatus
    {
        Active,     // Evento creado y vigente
        Cancelled,  // Evento cancelado por el administrador
        Completed   // Evento finalizado automáticamente cuando EndDate < DateTime.UtcNow (RN-06)
    }
}
