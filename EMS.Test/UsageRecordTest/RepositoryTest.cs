﻿using EquipmentManagementSystem.Data;
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
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 01, 04), IsDelete = true });
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
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01" });
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
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ03", ProjectName = "A", IsDelete = true });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ03", ProjectName = "B" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetLatestRecordOfProject("A");
                Assert.Equal("FXS-YZ02", project.InstrumentId);
            }
        }

        [Fact]
        public void GetLatestRecord_NoData_Should_ReturnNull()
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

        [Fact]
        public void GetMobilePhaseOrCarrierGasOfRecord_OneMobilePhase_Shoule_ReturnOne()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhaseOrCarrierGasOfRecord_OneMobilePhase_Shoule_ReturnOne))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), MobilePhase = "A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), MobilePhase = "A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), MobilePhase = "A" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetMobilePhaseOrCarrierGasOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Single(project);
                Assert.Equal("A", project['A']);
            }
        }

        [Fact]
        public void GetMobilePhaseOrCarrierGasOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhaseOrCarrierGasOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), MobilePhase = "A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), MobilePhase = "C" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), MobilePhase = "B" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetMobilePhaseOrCarrierGasOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Equal(3, project.Count);
            }
        }

        [Fact]
        public void GetMobilePhaseOrCarrierGasOfRecord_NotData_Shoule_ReturnEmptyDic()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhaseOrCarrierGasOfRecord_NotData_Shoule_ReturnEmptyDic))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetMobilePhaseOrCarrierGasOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Empty(project);
            }
        }

        [Fact]
        public void GetColumnTypeOfRecord_OneMobilePhase_Shoule_ReturnOne()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypeOfRecord_OneMobilePhase_Shoule_ReturnOne))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), ColumnType = "Type A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), ColumnType = "Type A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), ColumnType = "Type A" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetColumnTypeOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Single(project);
                Assert.Equal("Type A", project['A']);
            }
        }

        [Fact]
        public void GetColumnTypeOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypeOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), ColumnType = "Type A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), ColumnType = "Type C" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), ColumnType = "Type B" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetColumnTypeOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Equal(3, project.Count);
            }
        }

        [Fact]
        public void GetColumnTypeOfRecord_NotData_Shoule_ReturnEmptyDic()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypeOfRecord_NotData_Shoule_ReturnEmptyDic))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetColumnTypeOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Empty(project);
            }
        }

        [Fact]
        public void GetIonSourceOfRecord_OneMobilePhase_Shoule_ReturnOne()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourceOfRecord_OneMobilePhase_Shoule_ReturnOne))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), IonSource = "IonSource A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), IonSource = "IonSource A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), IonSource = "IonSource A" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetIonSourceOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Single(project);
                Assert.Equal("IonSource A", project['A']);
            }
        }

        [Fact]
        public void GetIonSourceOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourceOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), IonSource = "IonSource A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), IonSource = "IonSource C" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), IonSource = "IonSource B" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetIonSourceOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Equal(3, project.Count);
            }
        }

        [Fact]
        public void GetIonSourceOfRecord_NotData_Shoule_ReturnEmptyDic()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourceOfRecord_NotData_Shoule_ReturnEmptyDic))
                .Options;


            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetIonSourceOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Empty(project);
            }
        }

        [Fact]
        public void GetDetectorOfRecord_OneMobilePhase_Shoule_ReturnOne()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDetectorOfRecord_OneMobilePhase_Shoule_ReturnOne))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 01), Detector = "Detector A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 02), Detector = "Detector A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", BeginTime = new DateTime(2020, 12, 04), Detector = "Detector A" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetDetectorOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Single(project);
                Assert.Equal("Detector A", project['A']);
            }
        }

        [Fact]
        public void GetDetectorOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDetectorOfRecord_ThreeDifferentMobilePhase_Shoule_ReturnThree))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 01), Detector = "Detector A" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "2", BeginTime = new DateTime(2020, 12, 01), Detector = "Detector C" });
                context.UsageRecords.Add(new UsageRecord { InstrumentId = "FXS-YZ01", ProjectName = "1", BeginTime = new DateTime(2020, 12, 30), Detector = "Detector B" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetDetectorOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Equal(3, project.Count);
            }
        }

        [Fact]
        public void GetDetectorOfRecord_NotData_Shoule_ReturnEmptyDic()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDetectorOfRecord_NotData_Shoule_ReturnEmptyDic))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new UsageRecordRepository(context);
                var project = repo.GetDetectorOfRecord("FXS-YZ01", new DateTime(2020, 12, 01));
                Assert.Empty(project);
            }
        }
    }
}
