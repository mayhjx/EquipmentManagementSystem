using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services;
using RazorPagesTestSample.Tests;
using System;
using System.Threading.Tasks;
using Xunit;

namespace tests.AuditTrailServiceTest
{
    public class AuditTrailServiceTest
    {
        [Fact]
        public async Task Test_GetAuditTrailLog_AddOne()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var data1 = new UsageRecord() { BeginTime = new DateTime(2020, 01, 01, 01, 01, 00), ClinicSampleNumber = 1 };
            await db.UsageRecords.AddAsync(data1);
            await db.SaveChangesAsync();

            var service = new AuditTrailService(db);
            var actual = await service.GetAuditTrailLogsAsync(new UsageRecord().GetType().Name);

            Assert.Single(actual);
        }
    }
}
