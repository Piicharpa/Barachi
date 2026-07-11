using Microsoft.EntityFrameworkCore;
using Barachi.Models;

namespace Barachi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DIP");

            modelBuilder.Entity<BarachiEntity>()
                .ToTable("BARACHI");

            modelBuilder.Entity<BarachiDeleteLog>()
                .ToTable("BARACHIDELETELOGS");

            modelBuilder.Entity<KilldownRecord>()
                .ToTable("KILLDOWNRECORDS");

            modelBuilder.Entity<RetapingRecord>()
                .ToTable("RETAPINGRECORDS");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BarachiEntity> Barachi { get; set; }
        public DbSet<BarachiDeleteLog> BarachiDeleteLogs { get; set; }
        public DbSet<KilldownRecord> KilldownRecords { get; set; }
        public DbSet<RetapingRecord> RetapingRecords { get; set; }
    }
}