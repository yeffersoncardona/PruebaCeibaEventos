using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    public class EventosEnVivoDbContextFactory : IDesignTimeDbContextFactory<EventosEnVivoDbContext>
    {
        public EventosEnVivoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventosEnVivoDbContext>();

            // Coloca una cadena de conexión ficticia o de desarrollo local.
            // Solo se usará para evaluar la estructura de la base de datos durante la migración.
            optionsBuilder.UseSqlServer("Server=.;Database=EventosDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new EventosEnVivoDbContext(optionsBuilder.Options);
        }
    }
}
