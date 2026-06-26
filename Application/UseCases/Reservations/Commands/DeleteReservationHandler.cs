using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Reservations.Commands
{
    public class DeleteReservationCommand : IRequest<Guid>
    {
        public Guid ReservationId { get; set; }
    }
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, Guid>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Guid> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reserva = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reserva == null)
                throw new Exception("Reserva no encontrada");

            await _reservationRepository.DeleteAsync(reserva.Id);

            return reserva.Id; // devolvemos el Guid, no la entidad
        }
    }
}
