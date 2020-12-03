using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.UsageRecordTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetAllByInstrumentIdAndBeginTime_ReturnThreeRecords()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 01) });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 02) });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 03) });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ02", BeginTime = new DateTime(2020, 01, 01) });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var result = repo.GetAllByInstrumentIdAndBeginTime("FXS-YZ01", new DateTime(2020, 01, 01));
                Assert.Equal(3, result.Count);
            }
        }
    }
}
