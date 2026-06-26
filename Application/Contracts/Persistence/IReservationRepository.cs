using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(Guid id);
        Task<List<Reservation>> GetByEventIdAsync(Guid eventId);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Guid id);
    }
}
