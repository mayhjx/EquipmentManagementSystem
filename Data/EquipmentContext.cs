using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Data
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext(DbContextOptions<EquipmentContext> options)
            : base(options)
        {
        }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Calibration> Calibrations { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Assert> Asserts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UsageRecord> UsageRecords { get; set; }

        //public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<MaintenanceContent> MaintenanceContents { get; set; }


        //public DbSet<Computer> Computers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Calibration>().ToTable("Calibration");
            modelBuilder.Entity<Component>().ToTable("Component");
            modelBuilder.Entity<Assert>().ToTable("Assert");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<UsageRecord>().ToTable("UsageRecord")
                                                .Property(p => p.CreatedTime)
                                                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<MaintenanceContent>().ToTable("MaintenanceContent");

            //modelBuilder.Entity<Computer>().ToTable("Computer");
        }


        //public DbSet<Computer> Computers { get; set; }

        public DbSet<EquipmentManagementSystem.Models.MaintenanceRecord> MaintenanceRecord { get; set; }
    }
}
