using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly EventosEnVivoDbContext _context;

        public PaymentRepository(EventosEnVivoDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByReservationIdAsync(Guid reservationId)
        {
            return await _context.Payments
                                 .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
