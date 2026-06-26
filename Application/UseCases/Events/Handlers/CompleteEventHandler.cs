using Application.Contracts.Persistence;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class CompleteEventCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
    }

    public class CompleteEventHandler : IRequestHandler<CompleteEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public CompleteEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(CompleteEventCommand request, CancellationToken cancellationToken)
        {
            var evento = await _eventRepository.GetByIdAsync(request.EventId);

            if (evento == null)
                throw new Exception("Evento no encontrado");

            if (evento.EndDate < DateTime.UtcNow)
            {
                evento.MarkAsCompleted();
                await _eventRepository.UpdateAsync(evento);
            }

            return evento.Id;
        }
    }
}
