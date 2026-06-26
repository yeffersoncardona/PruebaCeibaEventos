using Application.Contracts.Persistence;
using Application.UseCases.Reservations.Command;
using Domain;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Reservations.Handlers
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IReservationRepository _reservationRepository;

        public CreateReservationHandler(IEventRepository eventRepository, IReservationRepository reservationRepository)
        {
            _eventRepository = eventRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // ✅ Usamos el método de fábrica
            var reserva = Reservation.Create(
                request.EventId,
                request.BuyerName,
                request.BuyerEmail,
                request.Quantity
            );

            await _reservationRepository.AddAsync(reserva);

            return reserva.Id; // devolvemos el identificador único
        }
    }
}
