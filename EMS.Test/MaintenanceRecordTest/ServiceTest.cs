using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using EquipmentManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.MaintenanceRecordTest
{
    public class ServiceTest
    {
        [Fact]
        public async Task GetDailyMaintenanceSituationOfMonth_Should_ReturnThreeRecord()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyMaintenanceSituationOfMonth_Should_ReturnThreeRecord))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ99", Platform = "LCMS" });

                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "C" });

                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 01), Daily = "A,B,C" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 02), Daily = "A,B" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 03), Daily = "A" });

                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var instrumentRepo = new InstrumentRepository(context);
                var recordRepo = new MaintenanceRecordRepository(context);
                var contentRepo = new MaintenanceContentRepository(context);
                var service = new MaintenanceRecordService(instrumentRepo, contentRepo, recordRepo);

                var result = await service.GetDailyMaintenanceSituationOfMonth("FXS-YZ99", new System.DateTime(2020, 12, 01));

                Assert.Equal(3, result.Count);
                Assert.Equal(new[] { "Y", "Y", "Y" }, result["A"].ToArray()[0..3]);
                Assert.Equal(new[] { "Y", "Y", "N" }, result["B"].ToArray()[0..3]);
                Assert.Equal(new[] { "Y", "N", "N" }, result["C"].ToArray()[0..3]);
            }
        }

        [Fact]
        public async Task GetDailyMaintenanceSituationOfMonth_NoInstrument_Should_ReturnEmptyDic()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyMaintenanceSituationOfMonth_NoInstrument_Should_ReturnEmptyDic))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                //context.Instruments.Add(new Instrument { ID = "FXS-YZ99", Platform = "LCMS" });

                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "C" });

                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 01), Daily = "A,B,C" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 02), Daily = "A,B" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 03), Daily = "A" });

                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var instrumentRepo = new InstrumentRepository(context);
                var recordRepo = new MaintenanceRecordRepository(context);
                var contentRepo = new MaintenanceContentRepository(context);
                var service = new MaintenanceRecordService(instrumentRepo, contentRepo, recordRepo);

                var result = await service.GetDailyMaintenanceSituationOfMonth("FXS-YZ99", new System.DateTime(2020, 12, 01));

                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetDailyMaintenanceSituationOfMonth_NoContent_Should_ReturnEmptyDic()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyMaintenanceSituationOfMonth_NoContent_Should_ReturnEmptyDic))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ99", Platform = "LCMS" });

                //context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                //context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "B" });
                //context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "C" });

                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 01), Daily = "A,B,C" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 02), Daily = "A,B" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 03), Daily = "A" });

                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var instrumentRepo = new InstrumentRepository(context);
                var recordRepo = new MaintenanceRecordRepository(context);
                var contentRepo = new MaintenanceContentRepository(context);
                var service = new MaintenanceRecordService(instrumentRepo, contentRepo, recordRepo);

                var result = await service.GetDailyMaintenanceSituationOfMonth("FXS-YZ99", new System.DateTime(2020, 12, 01));

                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetDailyMaintenanceSituationOfMonth_NoRecord_Should_DicReturnEmptyValue()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyMaintenanceSituationOfMonth_NoRecord_Should_DicReturnEmptyValue))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ99", Platform = "LCMS" });

                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "C" });

                //context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 01), Daily = "A,B,C" });
                //context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 02), Daily = "A,B" });
                //context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 03), Daily = "A" });

                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var instrumentRepo = new InstrumentRepository(context);
                var recordRepo = new MaintenanceRecordRepository(context);
                var contentRepo = new MaintenanceContentRepository(context);
                var service = new MaintenanceRecordService(instrumentRepo, contentRepo, recordRepo);

                var result = await service.GetDailyMaintenanceSituationOfMonth("FXS-YZ99", new System.DateTime(2020, 12, 01));

                Assert.Equal(new[] { "" }, result["A"].ToArray()[0..1]);
                Assert.Equal(new[] { "" }, result["B"].ToArray()[0..1]);
                Assert.Equal(new[] { "" }, result["C"].ToArray()[0..1]);
            }
        }

        [Fact]
        public void GetDailyMaintenanceOperatorOfMonth_ShouldBe_HasThreeOperatorInList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyMaintenanceOperatorOfMonth_ShouldBe_HasThreeOperatorInList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ99", Platform = "LCMS" });

                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "C" });

                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 01), Daily = "A,B,C", Operator = "Test 1" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 02), Daily = "A,B", Operator = "Test 1" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 03), Daily = "A", Operator = "Test 1" });

                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var instrumentRepo = new InstrumentRepository(context);
                var recordRepo = new MaintenanceRecordRepository(context);
                var contentRepo = new MaintenanceContentRepository(context);
                var service = new MaintenanceRecordService(instrumentRepo, contentRepo, recordRepo);

                var result = service.GetDailyMaintenanceOperatorOfMonth("FXS-YZ99", new System.DateTime(2020, 12, 01));

                Assert.Equal(31, result.Count);
                Assert.Equal(new[] { "Test 1", "Test 1", "Test 1", "" }, result.ToArray()[0..4]);
            }
        }

        [Fact]
        public async Task GetWeeklyMaintenanceSituationOfMonth_Should_ReturnThreeRecord()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetWeeklyMaintenanceSituationOfMonth_Should_ReturnThreeRecord))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ99", Platform = "LCMS" });

                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "C" });

                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 01), Weekly = "A,B,C" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 08), Weekly = "A,B" });
                context.MaintenanceRecords.Add(new MaintenanceRecord { InstrumentId = "FXS-YZ99", BeginTime = new System.DateTime(2020, 12, 16), Weekly = "A" });

                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var instrumentRepo = new InstrumentRepository(context);
                var recordRepo = new MaintenanceRecordRepository(context);
                var contentRepo = new MaintenanceContentRepository(context);
                var service = new MaintenanceRecordService(instrumentRepo, contentRepo, recordRepo);

                var result = await service.GetWeeklyMaintenanceSituationOfMonth("FXS-YZ99", new System.DateTime(2020, 12, 01));

                Assert.Equal(3, result.Count);
                Assert.Equal(new[] { "Y", "Y", "Y", "" }, result["A"]);
                Assert.Equal(new[] { "Y", "Y", "N", "" }, result["B"]);
                Assert.Equal(new[] { "Y", "N", "N", "" }, result["C"]);
            }
        }
    }
}
