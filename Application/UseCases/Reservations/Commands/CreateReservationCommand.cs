using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.UseCases.Reservations.Command
{
    public class CreateReservationCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string BuyerEmail { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
