using Domain.Entities;

namespace Application.Contracts.Persistence
{

    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(Guid id);
        Task<List<Event>> GetAllAsync();
        Task AddAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(Guid id);
    }
}
