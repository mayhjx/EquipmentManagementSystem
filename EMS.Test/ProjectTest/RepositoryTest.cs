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
        public async Task GetColumnTypesByShortName_ShouldBe_ReturnColumnType()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypesByShortName_ShouldBe_ReturnColumnType))
                .Options;

            var columnTypes = "A";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ShortName = "Test", ColumnType = columnTypes };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetColumnTypesByShortName("Test");
                Assert.Equal(columnTypes, result);
            }
        }

        [Fact]
        public async Task GetColumnTypesByShortName_NoData_ShouldBe_ReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetColumnTypesByShortName_NoData_ShouldBe_ReturnEmpty))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetColumnTypesByShortName("Test");
                Assert.Empty(result);
            }
        }


        [Fact]
        public async Task GetMobilePhasesByShortName_ShouldBe_ReturnMobilePhaseList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhasesByShortName_ShouldBe_ReturnMobilePhaseList))
                .Options;

            var mobilePhases = new List<string> { "A", "B" };

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ShortName = "Test" };
                project.SetMobilePhase(mobilePhases);
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetMobilePhasesByShortName("Test");
                Assert.Equal("A|B", result);
            }
        }

        [Fact]
        public async Task GetMobilePhasesByShortName_NoData_ShouldBe_ReturnEmptyString()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetMobilePhasesByShortName_NoData_ShouldBe_ReturnEmptyString))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetMobilePhasesByShortName("Test");
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetIonSourcesByShortName_ShouldBe_ReturnIonSource()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourcesByShortName_ShouldBe_ReturnIonSource))
                .Options;

            var ionSource = "A";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ShortName = "Test", IonSource = ionSource };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetIonSourcesByShortName("Test");
                Assert.Equal(ionSource, result);
            }
        }

        [Fact]
        public async Task GetIonSourcesByShortName_NoData_ShouldBe_ReturnEmptyString()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetIonSourcesByShortName_NoData_ShouldBe_ReturnEmptyString))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetIonSourcesByShortName("Test");
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetDetectorByName_ShouldBe_ReturnMobilePhase()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDetectorByName_ShouldBe_ReturnMobilePhase))
                .Options;

            var detector = "A";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ShortName = "Test", Detector = detector };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetDetectorsByShortName("Test");
                Assert.Equal(detector, result);
            }
        }

        [Fact]
        public async Task GetDetectorByShortName_NoData_ShouldBe_ReturnEmptyString()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetDetectorByShortName_NoData_ShouldBe_ReturnEmptyString))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetDetectorsByShortName("Test");
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetGroupNameByName_ShouldBe_ReturnGroupName()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetGroupNameByName_ShouldBe_ReturnGroupName))
                .Options;

            var groupName = "Group";

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ShortName = "Test", GroupName = groupName };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetGroupNameByShortName("Test");
                Assert.Equal(groupName, result);
            }
        }

        [Fact]
        public async Task GetGroupNameByName_NotSet_ReturnEmptyString()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetGroupNameByName_NotSet_ReturnEmptyString))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var project = new Project() { Name = "Test", ShortName = "Test" };
                context.Projects.Add(project);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetGroupNameByShortName("Test");
                Assert.Equal("", result);
            }
        }

        [Fact]
        public async Task GetGroupNameByName_NoData_ReturnEmptyString()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetGroupNameByName_NoData_ReturnEmptyString))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = await repo.GetGroupNameByShortName("Test");
                Assert.Equal("", result);
            }
        }

        [Fact]
        public void GetByGroup_Should_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetByGroup_Should_ReturnThree))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Projects.Add(new Project() { Name = "Project 1", GroupName = "Test" });
                context.Projects.Add(new Project() { Name = "Project 2", GroupName = "Test" });
                context.Projects.Add(new Project() { Name = "Project 3", GroupName = "Test" });
                context.Projects.Add(new Project() { Name = "Project 4", GroupName = "" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = repo.GetByGroup("Test");
                Assert.Equal(3, result.Count);
            }
        }

        [Fact]
        public void GetByGroup_NoData_Should_Empty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetByGroup_NoData_Should_Empty))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new ProjectRepository(context);
                var result = repo.GetByGroup("Test");
                Assert.Empty(result);
            }
        }
    }
}
