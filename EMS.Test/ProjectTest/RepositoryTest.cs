using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.ProjectTest
{
    public class RepositoryTest
    {
        [Fact]
        public async Task SetAndGet_ColumnTypesByName_ShouldBe_Same()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "SetAndGet_ColumnTypesByName_ShouldBe_Same")
                .Options;

            var columnTypes = new List<string> { "A", "B" };

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test" };
                project.SetColumnType(columnTypes);
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetColumnTypesByName("Test");
                Assert.Equal(columnTypes, result);
            }
        }


        [Fact]
        public async Task SetAndGet_MobilePhaseByName_ShouldBe_Same()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "SetAndGet_MobilePhaseByName_ShouldBe_Same")
                .Options;

            var mobilePhases = new List<string> { "A", "B" };

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test" };
                project.SetMobilePhase(mobilePhases);
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetMobilePhasesByName("Test");
                Assert.Equal(mobilePhases, result);
            }
        }
    }
}
