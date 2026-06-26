using MediatR;

namespace Application.UseCases.Events.Queries
{
    public class ReporteOcupacionQuery : IRequest<ReporteOcupacionDto>
    {
        public Guid EventId { get; set; }
    }
}
