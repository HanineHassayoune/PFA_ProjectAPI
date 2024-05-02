using PFA_ProjectAPI.Models.ActivityModels;
using PFA_ProjectAPI.Models.EventModels;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Models.EventModels;
namespace API.Data
{
    public class TBDbContext : DbContext
    {
        public TBDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Event> Events { get; set; }

        public DbSet<Activity> Activities { get; set; }

    }
}
