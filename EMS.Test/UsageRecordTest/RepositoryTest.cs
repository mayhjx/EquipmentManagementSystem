using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.UsageRecordTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetAllByInstrumentIdAndBeginTime_Should_ReturnThreeRecords()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_Should_ReturnThreeRecords))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 01) });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 02) });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 03) });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 04), IsDelete=true });
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

        [Fact]
        public void GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
                .Options;

            // Insert seed data into the database using one instance of the context

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var result = repo.GetAllByInstrumentIdAndBeginTime("FXS-YZ01", new DateTime(2020, 01, 01));
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task FakeDelete()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01"});
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = await repo.GetById(1);
                await repo.Delete(project);
            }

            using (var context = Utilities.CreateContext(options))
            {
                var project = await context.UsageRecords.SingleAsync();
                Assert.True(project.IsDelete);
            }
        }

        [Fact]
        public void GetLatestRecordOfProject_Should_ReturnLatestNoDeleteRecordOfProject()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ02", ProjectName = "A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ03", ProjectName = "A", IsDelete=true });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ03", ProjectName = "B" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project =  repo.GetLatestRecordOfProject("A");
                Assert.Equal("FXS-YZ02", project.InstrumentId);
            }
        }

        [Fact]
        public void GetLatestRecord_NoData_Should_ReturnNull ()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetLatestRecordOfProject("No Input");
                Assert.Null(project);
            }
        }
    }
}
