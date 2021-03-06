using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Data
{
    public class EquipmentContext : DbContext
    {
        private readonly IUserResolverService _userResolverService;
        private string _userId;
        private string _userName;
        public EquipmentContext(DbContextOptions<EquipmentContext> options, IUserResolverService userResolverService)
            : base(options)
        {
            _userResolverService = userResolverService;
            _userId = _userResolverService.GetUserId();
            _userName = _userResolverService.GetUserName();
        }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Calibration> Calibrations { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Assert> Asserts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<RecordTableSetting> RecordTableSettings { get; set; }
        public DbSet<UsageRecord> UsageRecords { get; set; }
        public DbSet<UsageRecordOfYuanSu> UsageRecordOfYuanSu { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<MaintenanceContent> MaintenanceContents { get; set; }
        public DbSet<InstrumentAcceptance> InstrumentAcceptances { get; set; }
        public DbSet<AuditTrailLog> AuditTrailLogs { get; set; }

        //public DbSet<Computer> Computers { get; set; }

        public DbSet<MalfunctionWorkOrder> MalfunctionWorkOrder { get; set; }
        public DbSet<MalfunctionInfo> MalfunctionInfo { get; set; }
        public DbSet<Investigation> Investigation { get; set; }
        public DbSet<RepairRequest> RepairRequest { get; set; }
        public DbSet<AccessoriesOrder> AccessoriesOrder { get; set; }
        public DbSet<Repair> Repair { get; set; }
        public DbSet<Validation> Validation { get; set; }
        public DbSet<MalfunctionPart> MalfunctionParts { get; set; }
        public DbSet<MalfunctionPhenomenon> MalfunctionPhenomenon { get; set; }
        public DbSet<MalfunctionReason> MalfunctionReason { get; set; }
        public DbSet<MalfunctionSolution> MalfunctionSolution { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().ToTable("Instrument");
            modelBuilder.Entity<Calibration>().ToTable("Calibration");
            modelBuilder.Entity<Component>().ToTable("Component");
            modelBuilder.Entity<Assert>().ToTable("Assert");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<UsageRecord>().ToTable("UsageRecord");
            modelBuilder.Entity<MaintenanceContent>().ToTable("MaintenanceContent");
            modelBuilder.Entity<MaintenanceRecord>().ToTable("MaintenanceRecords");
            modelBuilder.Entity<InstrumentAcceptance>().ToTable("InstrumentAcceptance");
            modelBuilder.Entity<AuditTrailLog>().ToTable("AuditTrailLog");

            modelBuilder.Entity<MalfunctionWorkOrder>().ToTable("MalfunctionWorkOrder")
                                                           .Property(p => p.CreatedTime)
                                                           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<MalfunctionInfo>().ToTable("MalfunctionInfo");
            modelBuilder.Entity<Investigation>().ToTable("Investigation");
            modelBuilder.Entity<RepairRequest>().ToTable("RepairRequest");
            modelBuilder.Entity<AccessoriesOrder>().ToTable("AccessoriesOrder");
            modelBuilder.Entity<Repair>().ToTable("Repair");
            modelBuilder.Entity<Validation>().ToTable("Validation");
            modelBuilder.Entity<MalfunctionPart>().ToTable("MalfunctionPart");
            modelBuilder.Entity<MalfunctionPhenomenon>().ToTable("MalfunctionPhenomenon");
            modelBuilder.Entity<MalfunctionReason>().ToTable("MalfunctionReason");
            modelBuilder.Entity<MalfunctionSolution>().ToTable("MalfunctionSolution");
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditTrailLog ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry)
                {
                    UserId = _userId,
                    UserName = _userName,
                    EntityName = entry.Entity.GetType().Name
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    // 获取属性的displayAttribute的Name
                    var propMemberInfo = entry.Entity.GetType().GetMember(property.Metadata.Name).FirstOrDefault();
                    string propertyName = GetDisplayName(propMemberInfo);

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.PrimaryKeyValue = property.CurrentValue.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.Action = EntityState.Added.ToString();
                            auditEntry.CurrentValue[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.Action = EntityState.Deleted.ToString();
                            auditEntry.OriginalValue[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                if(property.OriginalValue?.ToString() != property.CurrentValue?.ToString())
                                {
                                    auditEntry.Action = EntityState.Modified.ToString();
                                    auditEntry.OriginalValue[propertyName] = property.OriginalValue;
                                    auditEntry.CurrentValue[propertyName] = property.CurrentValue;
                                }
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties && _.Action != null))
            {
                AuditTrailLogs.Add(auditEntry.ToAudit());
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        public static string GetDisplayName(MemberInfo memberInfo)
        {
            // 返回DisplayAttribute名或者属性名
            var attrName = memberInfo.Name;
            DisplayAttribute attr = Attribute.GetCustomAttribute(memberInfo, typeof(DisplayAttribute)) as DisplayAttribute;

            if (attr != null)
            {
                attrName = attr.Name;
            }
            return attrName;
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.PrimaryKeyValue = prop.CurrentValue.ToString();
                    }
                    else
                    {
                        auditEntry.CurrentValue[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                AuditTrailLogs.Add(auditEntry.ToAudit());
            }
            return SaveChangesAsync();
        }
    }

    class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string Action { get; set; }
        public DateTime DateChanged { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string PrimaryKeyValue { get; set; }
        public Dictionary<string, object> OriginalValue { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> CurrentValue { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditTrailLog ToAudit()
        {
            var audit = new AuditTrailLog()
            {
                Action = Action,
                DateChanged = DateChanged,
                UserId = UserId,
                UserName = UserName,
                EntityName = EntityName,
                PrimaryKeyValue = PrimaryKeyValue,
                OriginalValue = OriginalValue.Count == 0 ? null : JsonConvert.SerializeObject(OriginalValue),
                CurrentValue = CurrentValue.Count == 0 ? null : JsonConvert.SerializeObject(CurrentValue),
            };
            return audit;
        }
    }
}
