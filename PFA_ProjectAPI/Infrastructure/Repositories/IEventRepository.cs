using PFA_ProjectAPI.Domain.Models.Domain;

namespace PFA_ProjectAPI.Infrastructure.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync(string? filterOn = null, string? filterQuery = null);


        Task<Event?> GetByIdAsync(Guid id);


        Task<Event> CreateAsync(Event evnt);


        Task<Event?> UpdateAsync(Guid id, Event evnt);

        Task<Event?> DeleteAsync(Guid id);
    }
}
