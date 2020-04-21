﻿using EquipmentManagementSystem.Models;
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
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Computer> Computers { get; set; }

        public DbSet<Malfunction> Malfunctions { get; set; }

        public DbSet<MalfunctionField> MalfunctionFields { get; set; }

        public DbSet<MalfunctionProblem> MalfunctionProblems { get; set; }

        public DbSet<MalfunctionReason> MalfunctionReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Calibration>().ToTable("Calibration");
            modelBuilder.Entity<Component>().ToTable("Component");
            modelBuilder.Entity<Assert>().ToTable("Assert");
            modelBuilder.Entity<ProjectTeam>().ToTable("ProjectTeam");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Malfunction>().ToTable("Malfunction");
            modelBuilder.Entity<MalfunctionField>().ToTable("MalfunctionField");
            modelBuilder.Entity<MalfunctionProblem>().ToTable("MalfunctionProblem");
            modelBuilder.Entity<MalfunctionReason>().ToTable("MalfunctionReason");

            modelBuilder.Entity<Computer>().ToTable("Computer");


            modelBuilder.Entity<Instrument>().Property(i => i.CreatedDate).ValueGeneratedOnAdd().HasDefaultValueSql("datetime('now', 'localtime')");
            modelBuilder.Entity<Instrument>().Property(i => i.ModifiedDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("datetime('now', 'localtime')");
        }

    }
}
