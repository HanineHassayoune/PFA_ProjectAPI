using API.Data;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Models.ActivityModels;

namespace PFA_ProjectAPI.Repositories
{
    public class SQLActivityRepository : IActivityRepository
    {
        private readonly TBDbContext dbContext;
        public SQLActivityRepository(TBDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }
        //implement the mtd that is in the interface
        public async Task<List<Activity>> GetAllAsync()
        {
            return await dbContext.Activities.ToListAsync();


            
        }
    }
}
