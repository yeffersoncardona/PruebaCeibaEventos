namespace Application.UseCases.Events.Queries
{
    public class ReporteOcupacionDto
    {
        public Guid EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public int TotalVendidas { get; set; }
        public int TotalDisponibles { get; set; }
        public double PorcentajeOcupacion { get; set; }
        public decimal Ingresos { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
