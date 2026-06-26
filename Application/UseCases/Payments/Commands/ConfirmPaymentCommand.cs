using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Payments.Commands
{
    public class ConfirmPaymentCommand : IRequest<Guid>
    {
        public Guid ReservationId { get; set; }
        public decimal Amount { get; set; }
    }
}
