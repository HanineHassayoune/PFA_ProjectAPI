using API.Data;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFA_ProjectAPI.Repositories
{
    public class SQLActivityRepository : IActivityRepository
    {
        private readonly TBDbContext dbContext;

        public SQLActivityRepository(TBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Activity> CreateAsync(Activity activity)
        {
            await dbContext.Activities.AddAsync(activity);
            await dbContext.SaveChangesAsync();
            return activity;
        }

        public async Task<Activity?> DeleteAsync(Guid id)
        {
            // Find the activity in the DB
            var existingActivity = await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
            if (existingActivity == null)
            {
                return null;
            }
            dbContext.Activities.Remove(existingActivity);
            await dbContext.SaveChangesAsync();
            return existingActivity;
        }

        // Implement the method that is in the interface
        public async Task<List<Activity>> GetAllAsync()
        {
            return await dbContext.Activities.ToListAsync();
        }

        public async Task<Activity?> GetByIdAsync(Guid id)
        {
            return await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Activity?> UpdateAsync(Guid id, Activity activity)
        {
            var existingActivity = await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
            if (existingActivity == null)
            {
                return null;
            }
            existingActivity.Title = activity.Title;
            existingActivity.StartDate = activity.StartDate;
            existingActivity.EndDate = activity.EndDate;
            existingActivity.Animator = activity.Animator;
            existingActivity.EventId = activity.EventId;
            existingActivity.Status = activity.Status;
            existingActivity.Description = activity.Description;
           
            await dbContext.SaveChangesAsync();
            return existingActivity;
        }


        public async Task<IEnumerable<Activity>> GetByEventIdAsync(Guid eventId)
        {
            return await dbContext.Activities
                .Where(activity => activity.EventId == eventId)
                .ToListAsync();
        }

    }
}
