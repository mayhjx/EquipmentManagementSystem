using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.MaintenanceRecordTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetLatestQuarterlyRecordOfInstrumentId()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetLatestQuarterlyRecordOfInstrumentId))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    InstrumentId = "FXS-YZ01",
                    BeginTime = new DateTime(2020, 01, 01),
                    Quarterly = "1"
                });
                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    InstrumentId = "FXS-YZ01",
                    BeginTime = new DateTime(2020, 01, 03),
                    Quarterly = "3"
                });
                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    InstrumentId = "FXS-YZ01",
                    BeginTime = new DateTime(2020, 01, 02),
                    Quarterly = "2"
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceRecordRepository(context);
                var result = repo.GetLatestQuarterlyRecordOfInstrumentId("FXS-YZ01");
                Assert.Equal("3", result.Quarterly);
            }
        }

        [Fact]
        public void GetLatestQuarterlyRecordOfInstrumentId_NoData_Should_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetLatestQuarterlyRecordOfInstrumentId_NoData_Should_ReturnNull))
                .Options;

            // Insert seed data into the database using one instance of the context

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceRecordRepository(context);
                var result = repo.GetLatestQuarterlyRecordOfInstrumentId("FXS-YZ01");
                Assert.Null(result);
            }
        }

        [Fact]
        public void GetLatestYearlyRecordOfInstrumentId()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetLatestYearlyRecordOfInstrumentId))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    InstrumentId = "FXS-YZ01",
                    BeginTime = new DateTime(2020, 01, 01),
                    Yearly = "1"
                });
                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    InstrumentId = "FXS-YZ01",
                    BeginTime = new DateTime(2020, 01, 03),
                    Yearly = "3"
                });
                context.MaintenanceRecords.Add(new MaintenanceRecord
                {
                    InstrumentId = "FXS-YZ01",
                    BeginTime = new DateTime(2020, 01, 02),
                    Yearly = "2"
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceRecordRepository(context);
                var result = repo.GetLatestYearlyRecordOfInstrumentId("FXS-YZ01");
                Assert.Equal("3", result.Yearly);
            }
        }

        [Fact]
        public void GetLatestYearlyRecordOfInstrumentId_NoData_Should_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetLatestYearlyRecordOfInstrumentId_NoData_Should_ReturnNull))
                .Options;

            // Insert seed data into the database using one instance of the context

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceRecordRepository(context);
                var result = repo.GetLatestYearlyRecordOfInstrumentId("FXS-YZ01");
                Assert.Null(result);
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
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ02", ProjectName = "A", SampleNumber = 100 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ02", ProjectName = "A", SampleNumber = 101 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ03", ProjectName = "A", SampleNumber = 102 });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetLatestRecordOfProject("A", "FXS-YZ02");
        //        Assert.Equal(101, project.SampleNumber);
        //    }
        //}

        //[Fact]
        //public void GetLatestRecord_NoData_Should_ReturnNull()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetLatestRecordOfProject("No project", "No instrument");
        //        Assert.Null(project);
        //    }
        //}

        //[Fact]
        //public void GetMobilePhaseOrCarrierGasOfRecord_OneMobilePhase_Shoule_ReturnOne()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetMobilePhaseOrCarrierGasOfRecord_OneMobilePhase_Shoule_ReturnOne))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), MobilePhase = "A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), MobilePhase = "A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), MobilePhase = "A" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetMobilePhaseOrCarrierGasOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Single(project);
        //        Assert.Equal("A", project['A']);
        //    }
        //}

        //[Fact]
        //public void GetMobilePhaseOrCarrierGasOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetMobilePhaseOrCarrierGasOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), MobilePhase = "A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), MobilePhase = "C" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), MobilePhase = "B" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetMobilePhaseOrCarrierGasOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Equal(3, project.Count);
        //    }
        //}

        //[Fact]
        //public void GetMobilePhaseOrCarrierGasOfRecord_NotData_Shoule_ReturnEmptyDic()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetMobilePhaseOrCarrierGasOfRecord_NotData_Shoule_ReturnEmptyDic))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetMobilePhaseOrCarrierGasOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Empty(project);
        //    }
        //}

        //[Fact]
        //public void GetColumnTypeOfRecord_ThreeSameMobilePhase_Shoule_ReturnOne()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetColumnTypeOfRecord_ThreeSameMobilePhase_Shoule_ReturnOne))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), ColumnType = "Type A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), ColumnType = "Type A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), ColumnType = "Type A" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetColumnTypeOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Single(project);
        //        Assert.Equal("Type A", project['A']);
        //    }
        //}

        //[Fact]
        //public void GetColumnTypeOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetColumnTypeOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), ColumnType = "Type A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), ColumnType = "Type C" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), ColumnType = "Type B" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetColumnTypeOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Equal(3, project.Count);
        //    }
        //}

        ////[Fact]
        ////public void GetColumnNumberOfRecord_Should_ReturnThree()
        ////{
        ////    var options = new DbContextOptionsBuilder<EquipmentContext>()
        ////        .UseInMemoryDatabase(databaseName: nameof(GetColumnNumberOfRecord_Should_ReturnThree))
        ////        .Options;

        ////    using (var context = Utilities.CreateContext(options))
        ////    {
        ////        context.UsageRecords.Add(new UsageRecord { 
        ////            InstrumentId = "FXS-YZ01",
        ////            ProjectName = "1", 
        ////            BeginTime = new DateTime(2020, 12, 01), 
        ////            SystemOneColumnNumber="Test-0",
        ////            SystemTwoColumnNumber="Test-1",
        ////        });
        ////        context.UsageRecords.Add(new UsageRecord { 
        ////            InstrumentId = "FXS-YZ01",
        ////            ProjectName = "2", 
        ////            BeginTime = new DateTime(2020, 12, 01),
        ////            SystemOneColumnNumber = "Test-2",
        ////            SystemTwoColumnNumber = "Test-3",
        ////        });
        ////        context.UsageRecords.Add(new UsageRecord
        ////        {
        ////            InstrumentId = "FXS-YZ01",
        ////            ProjectName = "3",
        ////            BeginTime = new DateTime(2020, 12, 01),
        ////            SystemOneColumnNumber = "Test-4",
        ////        });
        ////        context.SaveChanges();
        ////    }

        ////    using (var context = Utilities.CreateContext(options))
        ////    {
        ////        var repo = new UsageRecordRepository(context);
        ////        var project = repo.GetColumnNumberOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        ////        Assert.Equal(5, project.Count);
        ////    }
        ////}

        ////[Fact]
        ////public void GetColumnNumberOfRecord_NoData_Should_ReturnEmptyList()
        ////{
        ////    var options = new DbContextOptionsBuilder<EquipmentContext>()
        ////        .UseInMemoryDatabase(databaseName: nameof(GetColumnNumberOfRecord_NoData_Should_ReturnEmptyList))
        ////        .Options;

        ////    using (var context = Utilities.CreateContext(options))
        ////    {
        ////        var repo = new UsageRecordRepository(context);
        ////        var project = repo.GetColumnNumberOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        ////        Assert.Empty(project);
        ////    }
        ////}

        //[Fact]
        //public void GetColumnTypeOfRecord_NotData_Shoule_ReturnEmptyDic()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetColumnTypeOfRecord_NotData_Shoule_ReturnEmptyDic))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetColumnTypeOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Empty(project);
        //    }
        //}

        //[Fact]
        //public void GetIonSourceOfRecord_OneMobilePhase_Shoule_ReturnOne()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetIonSourceOfRecord_OneMobilePhase_Shoule_ReturnOne))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), IonSource = "IonSource A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), IonSource = "IonSource A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), IonSource = "IonSource A" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetIonSourceOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Single(project);
        //        Assert.Equal("IonSource A", project['A']);
        //    }
        //}

        //[Fact]
        //public void GetIonSourceOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetIonSourceOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), IonSource = "IonSource A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), IonSource = "IonSource C" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), IonSource = "IonSource B" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetIonSourceOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Equal(3, project.Count);
        //    }
        //}

        //[Fact]
        //public void GetIonSourceOfRecord_NotData_Shoule_ReturnEmptyDic()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetIonSourceOfRecord_NotData_Shoule_ReturnEmptyDic))
        //        .Options;


        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetIonSourceOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Empty(project);
        //    }
        //}

        //[Fact]
        //public void GetDetectorOfRecord_OneMobilePhase_Shoule_ReturnOne()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetDetectorOfRecord_OneMobilePhase_Shoule_ReturnOne))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), Detector = "Detector A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), Detector = "Detector A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), Detector = "Detector A" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetDetectorOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Single(project);
        //        Assert.Equal("Detector A", project['A']);
        //    }
        //}

        //[Fact]
        //public void GetDetectorOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetDetectorOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), Detector = "Detector A" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), Detector = "Detector C" });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), Detector = "Detector B" });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetDetectorOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Equal(3, project.Count);
        //    }
        //}

        //[Fact]
        //public void GetDetectorOfRecord_NotData_Shoule_ReturnEmptyDic()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetDetectorOfRecord_NotData_Shoule_ReturnEmptyDic))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var project = repo.GetDetectorOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
        //        Assert.Empty(project);
        //    }
        //}

        //[Fact]
        //public void GetTotalHoursOfRecords_ShouldBeEqual_Thirty()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalHoursOfRecords_ShouldBeEqual_Thirty))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), EndTime = new DateTime(2020, 12, 01, 18, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), EndTime = new DateTime(2020, 12, 02, 18, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00), EndTime = new DateTime(2020, 12, 03, 18, 00, 00) });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalUsageHoursOfRecords(records);
        //        Assert.Equal(30, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalHoursOfRecords_NoEndTime_ShouldBeEqual_Twenty()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalHoursOfRecords_NoEndTime_ShouldBeEqual_Twenty))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), EndTime = new DateTime(2020, 12, 01, 18, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), EndTime = new DateTime(2020, 12, 02, 18, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00) });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalUsageHoursOfRecords(records);
        //        Assert.Equal(20, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalSampleNumberOfRecords_ShouldBeEqual_300()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalSampleNumberOfRecords_ShouldBeEqual_300))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), SampleNumber = 100 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), SampleNumber = 100 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00), SampleNumber = 100 });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalSampleNumberOfRecords(records);
        //        Assert.Equal(300, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalSampleNumberOfRecords_NoData_ShouldBeEqual_0()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalSampleNumberOfRecords_NoData_ShouldBeEqual_0))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00) });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalSampleNumberOfRecords(records);
        //        Assert.Equal(0, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalBatchNumberOfRecords_ShouldBeEqual_9()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalBatchNumberOfRecords_ShouldBeEqual_9))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), SystemOneBatchNumber = 1, SystemTwoBatchNumber = 2 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), SystemOneBatchNumber = 1, SystemTwoBatchNumber = 2 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00), SystemOneBatchNumber = 1, SystemTwoBatchNumber = 2 });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalBatchNumberOfRecords(records);
        //        Assert.Equal(9, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalBatchNumberOfRecords_NoData_ShouldBeEqual_0()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalBatchNumberOfRecords_NoData_ShouldBeEqual_0))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00) });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalBatchNumberOfRecords(records);
        //        Assert.Equal(0, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalS1BatchNumberOfRecords_ShouldBeEqual_3()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalS1BatchNumberOfRecords_ShouldBeEqual_3))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), SystemOneBatchNumber = 1 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), SystemOneBatchNumber = 1 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00), SystemOneBatchNumber = 1 });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalS1BatchNumberOfRecords(records);
        //        Assert.Equal(3, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalS1BatchNumberOfRecords_NoData_ShouldBeEqual_0()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalS1BatchNumberOfRecords_NoData_ShouldBeEqual_0))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00) });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalS1BatchNumberOfRecords(records);
        //        Assert.Equal(0, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalS2BatchNumberOfRecords_ShouldBeEqual_3()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalS2BatchNumberOfRecords_ShouldBeEqual_3))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00), SystemTwoBatchNumber = 1 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00), SystemTwoBatchNumber = 1 });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00), SystemTwoBatchNumber = 1 });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalS2BatchNumberOfRecords(records);
        //        Assert.Equal(3, totalHours);
        //    }
        //}

        //[Fact]
        //public void GetTotalS2BatchNumberOfRecords_NoData_ShouldBeEqual_0()
        //{
        //    var options = new DbContextOptionsBuilder<EquipmentContext>()
        //        .UseInMemoryDatabase(databaseName: nameof(GetTotalS2BatchNumberOfRecords_NoData_ShouldBeEqual_0))
        //        .Options;

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02, 08, 00, 00) });
        //        context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 03, 08, 00, 00) });
        //        context.SaveChanges();
        //    }

        //    using (var context = Utilities.CreateContext(options))
        //    {
        //        var repo = new UsageRecordRepository(context);
        //        var records = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 12, 01));
        //        var totalHours = repo.GetTotalS2BatchNumberOfRecords(records);
        //        Assert.Equal(0, totalHours);
        //    }
        //}
    }
}
