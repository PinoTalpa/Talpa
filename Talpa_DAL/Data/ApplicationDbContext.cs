using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using Talpa_DAL.Entities;

namespace Talpa_DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserDto> Users { get; set; }
        public DbSet<RoleDto> Roles { get; set; }
        public DbSet<UserRoleDto> UserRoles { get; set; }
        public DbSet<SuggestionDto> Suggestions { get; set; }
        public DbSet<ChosenSuggestion> ChosenSuggestions { get; set; }
        public DbSet<VoteDto> Votes { get; set; }
        public DbSet<LimitationDto> Limitations { get; set; }
        public DbSet<ActivityLimitationDto> ActivityLimitations { get; set; }
        public DbSet<ActivityDateDto> ActivityDates { get; set; }
        public DbSet<UserActivityDateDto> UserActivityDates { get; set; }
    }
}