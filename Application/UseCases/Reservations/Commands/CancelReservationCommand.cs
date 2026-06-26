using MediatR;

namespace Application.UseCases.Reservations.Command
{
    public class CancelReservationCommand : IRequest<Guid>
    {
        public Guid ReservationId { get; set; }
    }
}
