﻿using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;
namespace EMS.Test.AuditTrailTest
{
    public class RepositoryTEst
    {
        [Fact]
        public async Task GetAuditTrailLogs_ByEntityName_Should_ReturnFour()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAuditTrailLogs_ByEntityName_Should_ReturnFour))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", Action = "Create" });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", Action = "Modified" });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", Action = "Delete" });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "2", Action = "Create" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new AuditTrailRepository(context);
                var result = await repo.GetAuditTrailLogs("UsageRecord");
                Assert.Equal(4, result.Count);
            }
        }

        [Fact]
        public async Task GetAuditTrailLogs_ByEntityNameAndId_Should_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAuditTrailLogs_ByEntityNameAndId_Should_ReturnThree))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", Action = "Create" });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", Action = "Modified" });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", Action = "Delete" });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "2", Action = "Create" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new AuditTrailRepository(context);
                var result = await repo.GetAuditTrailLogs("UsageRecord", 1);
                Assert.Equal(3, result.Count);
            }
        }

        [Fact]
        public async Task GetAuditTrailLogs_ByEntityNameAndIdAndDate_Should_ReturnFour()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAuditTrailLogs_ByEntityNameAndIdAndDate_Should_ReturnFour))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", DateChanged = new DateTime(2020, 12, 01) });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", DateChanged = new DateTime(2020, 12, 02) });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", DateChanged = new DateTime(2020, 12, 03) });
                context.AuditTrailLogs.Add(new AuditTrailLog { EntityName = "UsageRecord", PrimaryKeyValue = "1", DateChanged = new DateTime(2020, 12, 04) });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new AuditTrailRepository(context);
                var result = await repo.GetAuditTrailLogs("UsageRecord", 1, new DateTime(2020, 12, 01));
                Assert.Equal(4, result.Count);
            }
        }
    }
}