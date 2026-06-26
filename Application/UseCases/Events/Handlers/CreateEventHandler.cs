using Application.Contracts.Persistence;
using Application.UseCases.Events.Commands;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Events.Handlers
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
       

        public CreateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var ev = new Event
            {
                Title = request.Title,
                Description = request.Description,
                VenueId = request.VenueId, // 👈 solo referencia
                Capacity = request.Capacity,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Price = request.Price,
                Type = request.Type,
                Status = EventStatus.Active
            };

            await _eventRepository.AddAsync(ev);
            return ev.Id;
        }
    }
}
