using Application.Contracts.Persistence;
using Application.UseCases.Payments.Commands;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Payments.Handlers
{
    public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentRepository _paymentRepository;

        public ConfirmPaymentHandler(IReservationRepository reservationRepository, IPaymentRepository paymentRepository)
        {
            _reservationRepository = reservationRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            // Validar que la reserva exista
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
                throw new Exception("La reserva no existe.");

            // Validar estado de la reserva
            if (reservation.Status == ReservationStatus.Confirmed)
                throw new Exception("La reserva ya está confirmada.");
            if (reservation.Status == ReservationStatus.Cancelled)
                throw new Exception("La reserva fue cancelada y no puede confirmarse.");

            // Crear pago
            var payment = new Payment(
                          Guid.NewGuid(),
                          reservation.Id,
                          request.Amount,
                          PaymentStatus.Paid,
                          DateTime.UtcNow
                          );

            await _paymentRepository.AddAsync(payment);

            // Actualizar estado de la reserva
            reservation.ConfirmPayment();
            await _reservationRepository.UpdateAsync(reservation);

            return reservation.Id;
        }
    }
}
