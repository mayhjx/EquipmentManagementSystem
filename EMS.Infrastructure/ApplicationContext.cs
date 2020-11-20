using EMS.Core.Entities;
using EMS.Core.Entities.AcceptanceAggregate;
using EMS.Core.Entities.CalibrationAggregate;
using EMS.Core.Entities.GroupAggregate;
using EMS.Core.Entities.UsageRecordAggregate;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Component> Components { get; set; }

        public DbSet<Acceptance> Acceptances { get; set; }
        public DbSet<Calibration> Calibrations { get; set; }
        public DbSet<UsageRecord> UsageRecords { get; set; }
    }
}
