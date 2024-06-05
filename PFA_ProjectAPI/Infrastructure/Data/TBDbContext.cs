using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PFA_ProjectAPI.Domain.Models.Domain;

namespace PFA_ProjectAPI.Infrastructure.Data
{
    public class TBDbContext : DbContext
    {
        public TBDbContext(DbContextOptions<TBDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }


        public DbSet<Event> Events { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la relation one-to-many entre Activity et Image
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Images)
                .WithOne(i => i.Activity)
                .HasForeignKey(i => i.ActivityId);

            // Configuration de la relation one-to-many entre Event et Activity
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Activities)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId)
                .IsRequired();

            // Configuration de la relation one-to-one entre Event et Image
            modelBuilder.Entity<Event>()
                 .HasMany(e => e.Images)
                .WithOne(e => e.Event)
               .HasForeignKey(e => e.EventId);
            //.IsRequired();

            //Configuration de la relation one-to-many entre Event et feedback
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Feedbacks)
                .WithOne(f => f.Event)
                .HasForeignKey(f => f.EventId)
                .IsRequired();


            //Configuration de la relation one-to-many entre User et feedback
            modelBuilder.Entity<User>()
                .HasMany(e => e.Feedbacks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
            .IsRequired();


        modelBuilder.Entity<Event>()
       .HasMany(e => e.Users)
       .WithMany(e => e.Events);

        }
    }
}

