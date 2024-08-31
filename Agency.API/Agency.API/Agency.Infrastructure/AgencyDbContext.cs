using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure
{
    public class AgencyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<OffDay> OffDays { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        public AgencyDbContext(DbContextOptions<AgencyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Your PostgreSQL connection string here");
            }
        }
    }
}
