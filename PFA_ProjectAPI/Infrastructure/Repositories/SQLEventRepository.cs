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

        //apply sorting , filtring,an pagination in between these two lines
        public async Task<List<Event>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            var events = dbContext.Events.AsQueryable();

            // Filtring
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Inclure les activités pour chaque événement
            return await events.Include(e => e.Activities).ToListAsync();
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
