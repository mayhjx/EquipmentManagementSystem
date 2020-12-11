using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
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
    }
}
