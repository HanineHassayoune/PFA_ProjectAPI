using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PFA_ProjectAPI.Data
{
    public class TBAuthDbContext : IdentityDbContext
    {
        public TBAuthDbContext(DbContextOptions<TBAuthDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "8f79f010-4ba4-4c36-9353-3ed3e0fd476c";
            var writerRoleId = "fe1b38e9-ccfe-4d37-8ea7-49d1edd85a8d";
            var roles = new List<IdentityRole>
            {
                
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()

                },
                new IdentityRole
                {
                    Id =writerRoleId,
                    ConcurrencyStamp =writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
