using Application.Contracts.Persistence;
using Application.UseCases.Reservations.Command;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Reservations.Handlers
{
    public class CancelReservationHandler : IRequestHandler<CancelReservationCommand, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEventRepository _eventRepository;

        public CancelReservationHandler(IReservationRepository reservationRepository, IEventRepository eventRepository)
        {
            _reservationRepository = reservationRepository;
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
                throw new Exception("La reserva no existe.");

            if (reservation.Status == ReservationStatus.Cancelled)
                throw new Exception("La reserva ya está cancelada.");
            if (reservation.Status == ReservationStatus.PendingPayment)
                throw new Exception("No se puede cancelar una reserva pendiente de pago.");

            // Obtener evento asociado
            var ev = await _eventRepository.GetByIdAsync(reservation.EventId);
            if (ev == null)
                throw new Exception("El evento asociado no existe.");

            // RN‑07: Cancelación con penalización (<48 horas antes del evento)
            if ((ev.StartDate - DateTime.UtcNow).TotalHours < 48)
            {
                reservation.MarkAsLost(); // estado Lost, no libera entradas
            }
            else
            {
                reservation.Cancel(); // estado Cancelled, libera entradas y registra fecha/hora
            }

            await _reservationRepository.UpdateAsync(reservation);

            return reservation.Id;
        }
    }
}
