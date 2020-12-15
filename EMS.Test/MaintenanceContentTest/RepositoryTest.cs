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
        public void GetDailyContentByInstrumentPlatform_Should_ReturnThreeTextList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyContentByInstrumentPlatform_Should_ReturnThreeTextList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "A" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "B" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "日常维护", Text = "C" });
                context.MaintenanceContents.Add(new MaintenanceContent { InstrumentPlatform = "LCMS", Type = "周维护", Text = "A" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetDailyContentByInstrumentPlatform("LCMS");
                Assert.Equal(new List<string> { "A", "B", "C" }, result);
            }
        }

        [Fact]
        public void GetDailyContentByInstrumentPlatform_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDailyContentByInstrumentPlatform_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetDailyContentByInstrumentPlatform("LCMS");
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetWeeklyContentByInstrumentPlatform_Should_ReturnThreeTextList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetWeeklyContentByInstrumentPlatform_Should_ReturnThreeTextList))
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
                var result = repo.GetWeeklyContentByInstrumentPlatform("LCMS");
                Assert.Equal(new List<string> { "A", "B", "C" }, result);
            }
        }

        [Fact]
        public void GetWeeklyContentByInstrumentPlatform_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetWeeklyContentByInstrumentPlatform_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MaintenanceContentRepository(context);
                var result = repo.GetWeeklyContentByInstrumentPlatform("LCMS");
                Assert.Empty(result);
            }
        }
    }
}
