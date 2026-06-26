using Domain.Entities;
using MediatR;

namespace Application.UseCases.Events.Queries
{
    public class ListEventsQuery : IRequest<List<Event>>
    {
        public string? Type { get; set; }       // filtro por tipo de evento
        public DateTime? StartDate { get; set; } // filtro por fecha inicial
        public DateTime? EndDate { get; set; }   // filtro por fecha final
    }
}
