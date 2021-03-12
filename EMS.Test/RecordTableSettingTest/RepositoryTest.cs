using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.RecordTableSettingTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetUsageRecordSetting_ShouldReturn_Title()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetUsageRecordSetting_ShouldReturn_Title))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.RecordTableSettings.Add(new RecordTableSetting
                {
                    InstrumentID = "FXY-MS01",
                    UsageRecordTableChineseTitle = "Chinese Title",
                    UsageRecordTableEnglishTitle = "English Title",
                    UsageRecordTableNumber = "Number"
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new RecordTableSettingRepository(context);
                Assert.Equal("Chinese Title", repo.GetUsageRecordChineseTitle("FXY-MS01"));
                Assert.Equal("English Title", repo.GetUsageRecordEnglishTitle("FXY-MS01"));
                Assert.Equal("Number", repo.GetUsageRecordTableNumber("FXY-MS01"));
            }
        }

        [Fact]
        public void GetUsageRecordSetting_InstrumentNotExists_ShouldBe_Null()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetUsageRecordSetting_InstrumentNotExists_ShouldBe_Null))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.RecordTableSettings.Add(new RecordTableSetting
                {
                    InstrumentID = "FXY-MS01",
                    UsageRecordTableChineseTitle = "Chinese Title",
                    UsageRecordTableEnglishTitle = "English Title",
                    UsageRecordTableNumber = "Number"
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new RecordTableSettingRepository(context);
                Assert.Null(repo.GetUsageRecordChineseTitle("no exists"));
                Assert.Null(repo.GetUsageRecordEnglishTitle("no exists"));
                Assert.Null(repo.GetUsageRecordTableNumber("no exists"));
            }
        }

        [Fact]
        public void GetUsageRecordSetting_NoContent_ShouldBe_Null()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetUsageRecordSetting_NoContent_ShouldBe_Null))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new RecordTableSettingRepository(context);
                Assert.Null(repo.GetUsageRecordChineseTitle("no exists"));
                Assert.Null(repo.GetUsageRecordEnglishTitle("no exists"));
                Assert.Null(repo.GetUsageRecordTableNumber("no exists"));
            }
        }

        [Fact]
        public void GetMaintenanceRecordSetting_ShouldReturn_Title()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceRecordSetting_ShouldReturn_Title))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.RecordTableSettings.Add(new RecordTableSetting
                {
                    InstrumentID = "FXY-MS01",
                    MaintenanceRecordTableChineseTitle = "Chinese Title",
                    MaintenanceRecordTableEnglishTitle = "English Title",
                    MaintenanceRecordTableNumber = "Number"
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new RecordTableSettingRepository(context);
                Assert.Equal("Chinese Title", repo.GetMaintenanceRecordChineseTitle("FXY-MS01"));
                Assert.Equal("English Title", repo.GetMaintenanceRecordEnglishTitle("FXY-MS01"));
                Assert.Equal("Number", repo.GetMaintenanceRecordTableNumber("FXY-MS01"));
            }
        }

        [Fact]
        public void GetMaintenanceRecordSetting_InstrumentNotExists_ShouldBe_Null()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceRecordSetting_InstrumentNotExists_ShouldBe_Null))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.RecordTableSettings.Add(new RecordTableSetting
                {
                    InstrumentID = "FXY-MS01",
                    MaintenanceRecordTableChineseTitle = "Chinese Title",
                    MaintenanceRecordTableEnglishTitle = "English Title",
                    MaintenanceRecordTableNumber = "Number"
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new RecordTableSettingRepository(context);
                Assert.Null(repo.GetMaintenanceRecordChineseTitle("no exists"));
                Assert.Null(repo.GetMaintenanceRecordEnglishTitle("no exists"));
                Assert.Null(repo.GetMaintenanceRecordTableNumber("no exists"));
            }
        }

        [Fact]
        public void GetMaintenanceRecordSetting_NoContent_ShouldBe_Null()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceRecordSetting_NoContent_ShouldBe_Null))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new RecordTableSettingRepository(context);
                Assert.Null(repo.GetMaintenanceRecordChineseTitle("no exists"));
                Assert.Null(repo.GetMaintenanceRecordEnglishTitle("no exists"));
                Assert.Null(repo.GetMaintenanceRecordTableNumber("no exists"));
            }
        }
    }
}
