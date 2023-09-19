using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talpa_DAL.Entities;

namespace Talpa_DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Limitation> Limitations { get; set; }
        public DbSet<ActivityLimitation> ActivityLimitations { get; set; }
        public DbSet<Quarter> Quarters { get; set; }
        public DbSet<ActivityDate> ActivityDates { get; set; }
        public DbSet<UserActivityDate> UserActivityDates { get; set; }
    }
}