using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Data
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext (DbContextOptions<EquipmentContext> options)
            : base(options)
        {
        }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Calibration> Calibrations { get; set;}
        public DbSet<Component> components { get; set;}
        public DbSet<Assert> asserts { get; set;}
        public DbSet<ProjectTeam> projectTeams { get; set;}
        public DbSet<Project> projects { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Calibration>().ToTable("Calibration");
            modelBuilder.Entity<Component>().ToTable("Component");
            modelBuilder.Entity<Assert>().ToTable("Assert");
            modelBuilder.Entity<ProjectTeam>().ToTable("ProjectTeam");
            modelBuilder.Entity<Project>().ToTable("Project");

            modelBuilder.Entity<Instrument>().Property(i => i.CreatedDate).ValueGeneratedOnAdd().HasDefaultValueSql("datetime('now', 'localtime')");
            modelBuilder.Entity<Instrument>().Property(i => i.ModifiedDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("datetime('now', 'localtime')");
        }

        public DbSet<EquipmentManagementSystem.Models.Malfunction> Malfunction { get; set; }

        public DbSet<EquipmentManagementSystem.Models.Computer> Computer { get; set; }
    }
}
