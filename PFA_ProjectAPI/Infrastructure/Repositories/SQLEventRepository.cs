using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Domain.Models.Domain;
using PFA_ProjectAPI.Infrastructure.Data;
using System.Linq;

namespace PFA_ProjectAPI.Infrastructure.Repositories
{
    public class SQLEventRepository : IEventRepository
    {
        private readonly TBDbContext dbContext;
        public SQLEventRepository(TBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Event> CreateAsync(Event evnt)
        {
            await dbContext.Events.AddAsync(evnt);
            await dbContext.SaveChangesAsync();
            return evnt;


        }

        public async Task<Event?> DeleteAsync(Guid id)
        {
            var existingEvent = await dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (existingEvent == null)
            {
                return null;
            }
            dbContext.Events.Remove(existingEvent);
            await dbContext.SaveChangesAsync();
            return existingEvent;
        }

       
        public async Task<List<Event>> GetAllAsync(Guid userId)
        {
            var events = dbContext.Events.Where(evt => evt.Users.Select(user => user.Id).Contains(userId));
            return events.ToList();
            
        }


        public async Task<List<Event>> GetAllAsync()
        {
            var events = dbContext.Events;
            return events.ToList();

        }


        public async Task<Event?> GetByIdAsync(Guid id)
        {
            return await dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<Event?> UpdateAsync(Guid id, Event evnt)
        {
            var existingEvent = await dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEvent == null)
            {
                return null;
            }

            existingEvent.Name = evnt.Name;
            existingEvent.StartDate = evnt.StartDate;
            existingEvent.EndDate = evnt.EndDate;
            existingEvent.Creator = evnt.Creator;
            existingEvent.Capacity = evnt.Capacity;
            existingEvent.Status = evnt.Status;
            existingEvent.Category = evnt.Category;

            await dbContext.SaveChangesAsync();
            return existingEvent;
        }
    }
}
