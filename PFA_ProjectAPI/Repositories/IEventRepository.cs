using PFA_ProjectAPI.Models.Domain;

namespace PFA_ProjectAPI.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync(String?filterOn=null,String?filterQuery=null);


        Task<Event?> GetByIdAsync(Guid id);


        Task<Event> CreateAsync(Event evnt);


        Task<Event?> UpdateAsync(Guid id, Event evnt);

        Task<Event?> DeleteAsync(Guid id);
    }
}
