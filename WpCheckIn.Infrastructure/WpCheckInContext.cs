using Microsoft.EntityFrameworkCore;
using WpCheckIn.Entities;

namespace WpCheckIn.Infrastructure
{
    public class WpCheckInContext : DbContext
    {
        public DbSet<CheckIn> CheckIns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=34.226.175.244;Initial Catalog=WebPixCheckIn;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CheckIn>(ci =>
            {
                ci.Property(c => c.Descricao).HasColumnType("varchar(200)");
                ci.Property(c => c.Nome).HasColumnType("varchar(100)");
                ci.Property(c => c.Latitude).HasColumnType("varchar(100)");
                ci.Property(c => c.Longitude).HasColumnType("varchar(100)");
            });
        }
    }
}
