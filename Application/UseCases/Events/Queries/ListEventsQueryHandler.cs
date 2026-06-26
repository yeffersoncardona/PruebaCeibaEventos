using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Events.Queries
{
    public class ListEventsQueryHandler : IRequestHandler<ListEventsQuery, List<Event>>
    {
        private readonly IEventRepository _eventRepository;

        public ListEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Handle(ListEventsQuery request, CancellationToken cancellationToken)
        {
            // Aquí puedes aplicar filtros si los agregaste en ListEventsQuery
            var eventos = await _eventRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Type))
                eventos = eventos.Where(e => e.Title.Contains(request.Type)).ToList();

            if (request.StartDate.HasValue)
                eventos = eventos.Where(e => e.StartDate >= request.StartDate.Value).ToList();

            if (request.EndDate.HasValue)
                eventos = eventos.Where(e => e.EndDate <= request.EndDate.Value).ToList();

            return eventos;
        }
    }
}
