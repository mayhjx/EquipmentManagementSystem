using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.InstrumentTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetTestProjectsById_Should_ReturnProjectList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetTestProjectsById_Should_ReturnProjectList))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "Test", Projects = "A, B" });
                context.Instruments.Add(new Instrument { ID = "Other", Projects = "C, D" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetTestProjectsById("Test");
                Assert.Equal(new List<string> { "A", "B" }, result);
            }
        }

        [Fact]
        public void GetTestProjectsById_NoDataExists_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetTestProjectsById_NoDataExists_Should_ReturnEmptyList))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetTestProjectsById("Test");
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetAllInstrumentId_Should_ReturnAll()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllInstrumentId_Should_ReturnAll))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ01" });
                context.Instruments.Add(new Instrument { ID = "FXS-YZ02" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetAllInstrumentId();
                Assert.Equal(new List<string> { "FXS-YZ01", "FXS-YZ02" }, result);
            }
        }

        [Fact]
        public void GetAllInstrumentId_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllInstrumentId_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetAllInstrumentId();
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetAllInstrumentIdByGroup_Should_ReturnAllInstrument_BelongToTheGroup()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllInstrumentIdByGroup_Should_ReturnAllInstrument_BelongToTheGroup))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ01", Group = "VD" });
                context.Instruments.Add(new Instrument { ID = "FXS-YZ02", Group = "VD" });
                context.Instruments.Add(new Instrument { ID = "FXS-YZ03", Group = "VD" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetAllInstrumentIdByGroup("VD");
                Assert.Equal(new List<string> { "FXS-YZ01", "FXS-YZ02", "FXS-YZ03" }, result);
            }
        }

        [Fact]
        public void GetAllInstrumentIdByGroup_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllInstrumentIdByGroup_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetAllInstrumentIdByGroup("VD");
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetAllInstrumentIdByProject_Should_ReturnAllInstrument_HasTheProject()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllInstrumentIdByProject_Should_ReturnAllInstrument_HasTheProject))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "FXS-YZ01", Projects = "VD" });
                context.Instruments.Add(new Instrument { ID = "FXS-YZ02", Projects = "VD" });
                context.Instruments.Add(new Instrument { ID = "FXS-YZ03", Projects = "VD" });
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetAllInstrumentIdByProject("VD");
                Assert.Equal(new List<string> { "FXS-YZ01", "FXS-YZ02", "FXS-YZ03" }, result);
            }
        }

        [Fact]
        public void GetAllInstrumentIdByProject_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllInstrumentIdByProject_NoData_Should_ReturnEmptyList))
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetAllInstrumentIdByProject("VD");
                Assert.Empty(result);
            }
        }

        //[Fact]
        //public async Task GetModelById_Should_Return()
        //{
        //    // EntityFramework InMemory不是关系型数据库，无法实现外键关联
        //    //var options = new DbContextOptionsBuilder<EquipmentContext>()
        //    //    .UseInMemoryDatabase(databaseName: nameof(GetModelById_Should_Return))
        //    //    .Options;

        //    //using (var context = Utilities.CreateContext(options))
        //    //{
        //    //    var instrument = new Instrument { ID = "FXS-YZ01" };
        //    //    context.Instruments.Add(instrument);
        //    //    context.Components.Add(new Component { Instrument = instrument, InstrumentID = "FXS-YZ01", Model = "Test", Name = "A" });
        //    //    context.SaveChanges();
        //    //}

        //    //using (var context = Utilities.CreateContext(options))
        //    //{
        //    //    var repo = new InstrumentRepository(context);
        //    //    var result = await repo.GetModelById("FXS-YZ01");
        //    //    Assert.Equal("Test", result);
        //    //}
        //}
    }
}
