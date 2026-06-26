using Application.Contracts.Persistence;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Events.Queries
{
    public class ReporteOcupacionHandler : IRequestHandler<ReporteOcupacionQuery, ReporteOcupacionDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReporteOcupacionHandler(IEventRepository eventRepository, IReservationRepository reservationRepository)
        {
            _eventRepository = eventRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<ReporteOcupacionDto> Handle(ReporteOcupacionQuery request, CancellationToken cancellationToken)
        {
            var ev = await _eventRepository.GetByIdAsync(request.EventId);
            if (ev == null) throw new Exception("Evento no encontrado.");

            var reservas = await _reservationRepository.GetByEventIdAsync(ev.Id);

            var totalVendidas = reservas.Where(r => r.Status == ReservationStatus.Confirmed).Sum(r => r.Quantity);
            var totalPerdidas = reservas.Where(r => r.Status == ReservationStatus.Lost).Sum(r => r.Quantity);
            var totalDisponibles = ev.Capacity - totalVendidas - totalPerdidas;

            var ingresos = totalVendidas * ev.Price;
            var porcentajeOcupacion = ev.Capacity > 0 ? (double)totalVendidas / ev.Capacity * 100 : 0;

            return new ReporteOcupacionDto
            {
                EventId = ev.Id,
                EventTitle = ev.Title,
                TotalVendidas = totalVendidas,
                TotalDisponibles = totalDisponibles,
                PorcentajeOcupacion = porcentajeOcupacion,
                Ingresos = ingresos,
                Estado = ev.Status.ToString()
            };
        }
    }
}