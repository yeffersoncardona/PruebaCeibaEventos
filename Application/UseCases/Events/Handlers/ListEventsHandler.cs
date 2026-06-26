using Application.Contracts.Persistence;
using Application.UseCases.Events.Queries;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Events.Handlers
{
    public class ListEventsHandler : IRequestHandler<ListEventsQuery, List<Event>>
    {
        private readonly IEventRepository _eventRepository;

        public ListEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Handle(ListEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsync();

            // aplicar filtros si existen
            if (!string.IsNullOrEmpty(request.Type))
                events = events.Where(e => e.Type.Equals(request.Type, StringComparison.OrdinalIgnoreCase)).ToList();

            if (request.StartDate.HasValue)
                events = events.Where(e => e.StartDate >= request.StartDate.Value).ToList();

            if (request.EndDate.HasValue)
                events = events.Where(e => e.EndDate <= request.EndDate.Value).ToList();

            return events;
        }
    }
}
