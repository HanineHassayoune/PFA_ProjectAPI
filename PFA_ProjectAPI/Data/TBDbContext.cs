using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Models.Domain;
using PFA_ProjectAPI.Models.Enums;
using System;
using System.Reflection.Metadata;

namespace API.Data
{
    public class TBDbContext : DbContext
    {
        public TBDbContext(DbContextOptions<TBDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public DbSet<Image> Images{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //one to many 
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Activities) // Un événement a plusieurs activités
                .WithOne(a => a.Event)       // Une activité appartient à un seul événement
                .HasForeignKey(a => a.EventId) // Clé étrangère dans la table des activités
                .IsRequired();


            // Configure one-to-one relationship
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Image)
                .WithOne(e => e.Event)
                .HasForeignKey<Image>(e => e.EventId)
                .IsRequired();

        }
    }
}
