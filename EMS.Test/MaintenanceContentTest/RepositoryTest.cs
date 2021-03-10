using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.MaintenanceContentTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetByInstrumentPlatform_Should_ReturnThreeMaintenanceContent()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetByInstrumentPlatform_Should_ReturnThreeMaintenanceContent))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "A" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetByInstrumentPlatform("A");
                Assert.Equal(3, result.Count);
            }
        }

        [Fact]
        public void GetByInstrumentPlatform_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetByInstrumentPlatform_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetByInstrumentPlatform("A");
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetMaintenanceContentByInstrumentPlatform_Should_ReturnThreeTextList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceContentByInstrumentPlatform_Should_ReturnThreeTextList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "C" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetMaintenanceContentByInstrumentPlatform("LCMS", "周维护");
                Assert.Equal(new List<string> { "A", "B", "C" }, result);
            }
        }

        [Fact]
        public void GetMaintenanceContentByInstrumentPlatform_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceContentByInstrumentPlatform_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetMaintenanceContentByInstrumentPlatform("LCMS", "周维护");
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetMaintenanceCycleOfPlatform()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceCycleOfPlatform))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "季度维护", Text = "A", Cycle = 181 });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "季度维护", Text = "B", Cycle = 180 });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetMaintenanceCycleOfPlatform("LCMS", "季度维护", "B");
                Assert.Equal(180, result);
            }
        }

        [Fact]
        public void GetMaintenanceCycleOfPlatform_NoData_ShouldReturnZero()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMaintenanceCycleOfPlatform_NoData_ShouldReturnZero))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetMaintenanceCycleOfPlatform("LCMS", "季度维护", "B");
                Assert.Equal(int.MaxValue, result);
            }
        }

        [Fact]
        public void GetRemindTimeOfPlatform()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetRemindTimeOfPlatform))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "季度维护", Text = "A", RemindTime = 31 });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "季度维护", Text = "B", RemindTime = 30 });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetRemindTimeOfPlatform("LCMS", "季度维护", "B");
                Assert.Equal(30, result);
            }
        }

        [Fact]
        public void GetRemindTimeOfPlatform_NoData_ShouldReturnZero()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetRemindTimeOfPlatform_NoData_ShouldReturnZero))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetRemindTimeOfPlatform("LCMS", "季度维护", "B");
                Assert.Equal(0, result);
            }
        }
    }
}
