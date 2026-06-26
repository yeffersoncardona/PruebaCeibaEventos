# Documento de Trazabilidad RN ↔ Pruebas

Este documento describe cómo cada **Regla de Negocio (RN)** definida en el enunciado está cubierta por pruebas automatizadas en el proyecto **EventosEnVivo**.

---

## RN‑01: Capacidad del evento ≤ capacidad del venue
- **Descripción**: Un evento no puede exceder la capacidad del venue.
- **Prueba**: `EventCapacityTests.CrearEvento_ConCapacidadMayorQueVenue_DeberiaFallar`
- **Resultado esperado**: Lanza `DomainException` si la capacidad del evento > capacidad del venue.

---

## RN‑02: No solapar horarios en mismo venue
- **Descripción**: Dos eventos en el mismo venue no pueden tener horarios que se solapen.
- **Prueba**: `EventScheduleConflictTests.CrearEvento_ConConflictoDeHorarios_DeberiaFallar`
- **Resultado esperado**: Lanza `DomainException` si los horarios se cruzan.

---

## RN‑03: Eventos fin de semana no inician después de 22:00
- **Descripción**: Un evento sábado o domingo no puede iniciar después de las 22:00.
- **Prueba**: `EventWeekendTests.CrearEvento_FinDeSemanaDespuesDe22_DeberiaFallar`
- **Resultado esperado**: Lanza `DomainException` si el evento inicia >22h en fin de semana.

---

## RN‑04: Confirmar reserva genera código único
- **Descripción**: Al confirmar una reserva se asigna un código único.
- **Prueba**: `ConfirmPaymentHandlerTests.ConfirmarPago_DeberiaCambiarEstadoReservaAConfirmed`
- **Resultado esperado**: Estado → `Confirmed` y `ReservationCode` generado con prefijo `"EV-"`.

---

## RN‑05: Confirmar reserva cambia estado a Confirmed
- **Descripción**: Una reserva pendiente de pago pasa a estado `Confirmed` al confirmar.
- **Prueba**: `ConfirmPaymentHandlerTests.ConfirmarPago_DeberiaCambiarEstadoReservaAConfirmed`
- **Resultado esperado**: Estado → `Confirmed`.

---

## RN‑06: Evento se marca como completado si pasó EndDate
- **Descripción**: Un evento cuya fecha de finalización ya pasó se marca como `Completed`.
- **Prueba**: `CompleteEventHandlerTests.CompletarEvento_DeberiaCambiarEstadoACompleted_SiEndDateEsPasado`
- **Resultado esperado**: Estado → `Completed`.

---

## RN‑07: Cancelación con penalización (<48h → Lost)
- **Descripción**: Una reserva confirmada cancelada <48h antes del evento se marca como `Lost`. ≥48h → `Cancelled`.
- **Prueba**: 
  - `CancelReservationHandlerTests.CancelarReserva_DeberiaCambiarEstadoA_Cancelled` (≥48h)  
  - `CancelReservationHandlerTests.CancelarReserva_DeberiaCambiarEstadoA_Lost_SiEventoEsMenosDe48Horas` (<48h)
- **Resultado esperado**: Estado → `Lost` o `Cancelled` según el tiempo restante.

---

## ✅ Conclusión
Todas las reglas de negocio RN‑01 a RN‑07 están implementadas y validadas mediante pruebas automatizadas en el proyecto. Este documento asegura la trazabilidad entre requisitos y pruebas, garantizando cobertura completa.
