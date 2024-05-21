using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using PFA_ProjectAPI.Models.Domain;
namespace API.Data
{
    public class TBDbContext : DbContext
    {
        public TBDbContext(DbContextOptions<TBDbContext>dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Event> Events { get; set; }

        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Activity
            var activities=new List<Activity>()
            {
                new Activity
                {
                    Id=Guid.Parse("0f679471-a668-46bd-914b-0b0e7b493a3b"),
                    Title="The first title",
                    StartDate ="The start date ",
                    EndDate="The end date ",
                    Animator ="The animator",
                    Pictures ="The pictures",
                    TeamBuilding="The tembuilding",
                    Status =0,
                    Description="Les descriptions ",
                }
            };

            //Seed activities to the database
            modelBuilder.Entity<Activity>().HasData(activities);    


        }

    }
}
