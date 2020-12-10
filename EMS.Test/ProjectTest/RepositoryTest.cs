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
        public async Task GetShortNamesByNames_Should_ReturnShortNameList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetShortNamesByNames_Should_ReturnShortNameList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Projects.AddRange(new Project[]{
                    new Project{Name = "25-OHD", ShortName = "VD"},
                    new Project{Name = "甲氧基肾上腺素", ShortName = "MNs"},
                });
                await context.SaveChangesAsync();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetShortNamesByNames(new List<string> { "25-OHD", "甲氧基肾上腺素" });
                Assert.Equal(new List<string> { "VD", "MNs" }, result);
            }
        }

        [Fact]
        public async Task GetShortsNameByNames_NotDataExists_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetShortsNameByNames_NotDataExists_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetShortNamesByNames(new List<string> { });
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetColumnTypesByName_ShouldBe_ReturnColumnTypeList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypesByName_ShouldBe_ReturnColumnTypeList))
                .Options;

            var columnTypes = "A";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ColumnType = columnTypes };
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
        public async Task GetColumnTypesByName_NoData_ShouldBe_ReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypesByName_NoData_ShouldBe_ReturnEmpty))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetColumnTypesByName("Test");
                Assert.Empty(result);
            }
        }


        [Fact]
        public async Task GetMobilePhasesByName_ShouldBe_ReturnMobilePhaseLis()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhasesByName_ShouldBe_ReturnMobilePhaseLis))
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

        [Fact]
        public async Task GetMobilePhasesByName_NoData_ShouldBe_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhasesByName_NoData_ShouldBe_ReturnEmptyList))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetMobilePhasesByName("Test");
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetIonSourcesByName_ShouldBe_ReturnMobilePhaseLis()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourcesByName_ShouldBe_ReturnMobilePhaseLis))
                .Options;

            var ionSource = "A";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", IonSource = ionSource };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetIonSourcesByName("Test");
                Assert.Equal(ionSource, result);
            }
        }

        [Fact]
        public async Task GetIonSourcesByName_NoData_ShouldBe_ReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourcesByName_NoData_ShouldBe_ReturnEmpty))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetIonSourcesByName("Test");
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetDetectorByName_ShouldBe_ReturnMobilePhaseList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDetectorByName_ShouldBe_ReturnMobilePhaseList))
                .Options;

            var detector = "A";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", Detector = detector };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetDetectorsByName("Test");
                Assert.Equal(detector, result);
            }
        }

        [Fact]
        public async Task GetDetectorByName_NoData_ShouldBe_ReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypesByName_NoData_ShouldBe_ReturnEmpty))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetDetectorsByName("Test");
                Assert.Empty(result);
            }
        }
    }
}
