using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Data
{
    public class MalfunctionContext : DbContext
    {
        public MalfunctionContext(DbContextOptions<MalfunctionContext> options)
            : base(options)
        {
        }

        public DbSet<MalfunctionWorkOrder> MalfunctionWorkOrder { get; set; }
        public DbSet<MalfunctionInfo> MalfunctionInfo { get; set; }
        public DbSet<Investigation> Investigation { get; set; }
        public DbSet<RepairRequest> RepairRequest { get; set; }
        public DbSet<AccessoriesOrder> AccessoriesOrder { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<Validation> Validation { get; set; }
        public DbSet<MalfunctionPart> MalfunctionParts { get; set; }
        public DbSet<MalfunctionPhenomenon> MalfunctionPhenomenon { get; set; }
        public DbSet<MalfunctionReason> MalfunctionReason { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MalfunctionWorkOrder>().ToTable("MalfunctionWorkOrder")
                                                            .Property(p => p.CreatedTime)
                                                            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<MalfunctionInfo>().ToTable("MalfunctionInfo");
            modelBuilder.Entity<Investigation>().ToTable("Investigation");
            modelBuilder.Entity<RepairRequest>().ToTable("RepairRequest");
            modelBuilder.Entity<AccessoriesOrder>().ToTable("AccessoriesOrder");
            modelBuilder.Entity<Maintenance>().ToTable("Maintenance");
            modelBuilder.Entity<Validation>().ToTable("Validation");
            modelBuilder.Entity<MalfunctionPart>().ToTable("MalfunctionPart");
            modelBuilder.Entity<MalfunctionPhenomenon>().ToTable("MalfunctionPhenomenon");
            modelBuilder.Entity<MalfunctionReason>().ToTable("MalfunctionReason");
        }
    }
}
