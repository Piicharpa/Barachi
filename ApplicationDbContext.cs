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
            modelBuilder.HasDefaultSchema("DIP1");

            modelBuilder.Entity<BarachiEntity>()
                .ToTable("BARACHI");

            modelBuilder.Entity<BarachiDeleteLog>()
                .ToTable("BARACHIDELETELOGS");

            modelBuilder.Entity<KilldownRecord>()
                .ToTable("KILLDOWNRECORDS");

            modelBuilder.Entity<RetapingRecord>()
                .ToTable("RETAPINGRECORDS");

            modelBuilder.Entity<BarachiEntity>(entity =>
                {
                entity.ToTable("BARACHI");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.RBID).HasColumnName("RBID");
                entity.Property(e => e.LotNumber).HasColumnName("LOTNUMBER");
                entity.Property(e => e.Type).HasColumnName("TYPE");
                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.CreatedDate).HasColumnName("CREATEDDATE");
                entity.Property(e => e.ModifiedDate).HasColumnName("MODIFIEDDATE");
                });

            modelBuilder.Entity<BarachiDeleteLog>(entity =>
                {
                entity.ToTable("BARACHIDELETELOGS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.RBID).HasColumnName("RBID");
                entity.Property(e => e.PIC).HasColumnName("PIC");
                entity.Property(e => e.Reasons).HasColumnName("REASONS");
                entity.Property(e => e.DeletedDate).HasColumnName("DELETEDDATE");
                entity.Property(e => e.NextStep).HasColumnName("NEXTSTEP");
                });

            modelBuilder.Entity<KilldownRecord>(entity =>
                {
                entity.ToTable("KILLDOWNRECORDS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.RBID).HasColumnName("RBID");
                entity.Property(e => e.LotNumber).HasColumnName("LOTNUMBER");
                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.AcknowledgedDate).HasColumnName("ACKNOWLEDGEDDATE");
                });

            modelBuilder.Entity<RetapingRecord>(entity =>
                {
                entity.ToTable("RETAPINGRECORDS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.RBID).HasColumnName("RBID");
                entity.Property(e => e.LotNumber).HasColumnName("LOTNUMBER");
                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.AcknowledgedDate).HasColumnName("ACKNOWLEDGEDDATE");
                entity.Property(e => e.Instructions).HasColumnName("INSTRUCTIONS");
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BarachiEntity> Barachi { get; set; }
        public DbSet<BarachiDeleteLog> BarachiDeleteLogs { get; set; }
        public DbSet<KilldownRecord> KilldownRecords { get; set; }
        public DbSet<RetapingRecord> RetapingRecords { get; set; }
    }
}