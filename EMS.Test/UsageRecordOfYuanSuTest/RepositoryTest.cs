using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.UsageRecordOfYuanSuTest
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
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 01, 01) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 01, 02) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 01, 03) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS06", BeginTime = new DateTime(2020, 01, 01) });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordOfYuanSuRepository(context);
                var result = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXY-MS05", new DateTime(2020, 01, 01));
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
                var repo = new UsageRecordOfYuanSuRepository(context);
                var result = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXY-MS05", new DateTime(2020, 01, 01));
                Assert.Empty(result);
            }
        }

        //[Fact]
        //public void GetLatestRecordOfProject_Should_ReturnLatestNoDeleteRecordOfProject()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentID = "FXS-YZ02", ProjectName = "A", SampleNumber = 100 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentID = "FXS-YZ02", ProjectName = "A", SampleNumber = 101 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentID = "FXS-YZ03", ProjectName = "A", SampleNumber = 102 });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetLatestRecordOfProject("A", "FXS-YZ02");
        //        Assert.Equal(101, project.SampleNumber);
        //    }
        //}

        [Fact]
        public void GetTotalHoursOfRecords_ShouldBeEqual_Thirty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetTotalHoursOfRecords_ShouldBeEqual_Thirty))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), EndTime = new DateTime(2020, 12, 01, 18, 00, 00) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), EndTime = new DateTime(2020, 12, 02, 18, 00, 00) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00), EndTime = new DateTime(2020, 12, 03, 18, 00, 00) });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordOfYuanSuRepository(context);
                var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXY-MS05", new DateTime(2020, 12, 01));
                var totalHours = repo.GetTotalUsageHoursOfRecords(records);
                Assert.Equal(30, totalHours);
            }
        }

        [Fact]
        public void GetTotalHoursOfRecords_NoEndTime_ShouldBeEqual_Twenty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetTotalHoursOfRecords_NoEndTime_ShouldBeEqual_Twenty))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), EndTime = new DateTime(2020, 12, 01, 18, 00, 00) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), EndTime = new DateTime(2020, 12, 02, 18, 00, 00) });
                context.UsageRecordOfYuanSu.Add(new UsageRecordOfYuanSu { InstrumentID = "FXY-MS05", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00) });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordOfYuanSuRepository(context);
                var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXY-MS05", new DateTime(2020, 12, 01));
                var totalHours = repo.GetTotalUsageHoursOfRecords(records);
                Assert.Equal(20, totalHours);
            }
        }
    }
}
