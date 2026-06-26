# 🎟️ WebApiEventosEnVivo - Backend (.NET)

Backend desarrollado en **.NET 10 / C#** siguiendo principios de **Clean Architecture** para gestionar eventos y reservas con reglas de negocio validadas mediante pruebas automatizadas.

---

## 🚀 Instrucciones para ejecutar localmente

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/<tu_usuario>/PruebaCeibaEventos.git
   cd PruebaCeibaEventos
   
2. **Configurar la base de datos**

Por defecto: InMemory (no requiere configuración).

Para SQL Server: actualizar la cadena de conexión en appsettings.json.

3. **Aplicar migraciones (EF Core)**

⚠️ Importante: ejecutar siempre desde la carpeta raíz de la solución (PruebaCeibaEventos).

Crear migración inicial:

 dotnet ef migrations add InitialCreate -p Infrastructure -s WebApiEventosEnVivo --context EventosEnVivoDbContext
 
Aplicar migraciones a la base de datos:
 
 dotnet ef database update -p Infrastructure -s WebApiEventosEnVivo --context EventosEnVivoDbContext
 
Restaurar dependencias
   ```bash
   dotnet restore

4. **Ejecutar Api**
   ```bash
  dotnet run --project WebApiEventosEnVivo.API

5. **Acceder a la Api**
   https://localhost:7100/scalar/v1

**Arquitectura elegida**
Se implementó Clean Architecture con separación de capas:

Domain → Entidades y reglas de negocio (RN‑01 a RN‑07).

Application → Casos de uso, comandos y handlers (CQRS + MediatR).

Infrastructure → Persistencia con EF Core, repositorios y configuración.

API → Endpoints RESTful expuestos con ASP.NET Core.

Justificación: esta arquitectura asegura bajo acoplamiento, alta mantenibilidad y escalabilidad, cumpliendo principios SOLID y buenas prácticas de diseño.

**Tecnologías utilizadas**
NET 8 / C#

ASP.NET Core Web API

Entity Framework Core

MediatR (CQRS)

xUnit + Moq (tests automatizados)

Swagger/OpenAPI (documentación de endpoints)

**Tests automatizados**
Se implementaron pruebas unitarias e integración que validan todas las reglas de negocio:

RN‑01: Capacidad del evento ≤ venue

RN‑02: No solapar horarios

RN‑03: Eventos fin de semana ≤22h

RN‑04: Confirmar reserva genera código único

RN‑05: Confirmar reserva cambia estado a Confirmed

RN‑06: Evento se marca como Completed si pasó EndDate

RN‑07: Cancelación <48h → Lost, ≥48h → Cancelled

**Ejecutar pruebas**

dotnet test


