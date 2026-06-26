using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByReservationIdAsync(Guid reservationId);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
    }
}
