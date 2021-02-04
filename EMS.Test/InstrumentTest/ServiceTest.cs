using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using EquipmentManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.InstrumentTest
{
    public class ServiceTest
    {
        [Fact]
        public void GetInstrumentIdRelateToProjectsOfGroup_Should_ReturnThree()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetInstrumentIdRelateToProjectsOfGroup_Should_ReturnThree))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Projects.Add(new Project { Name = "A", GroupName = "Group A" });
                context.Projects.Add(new Project { Name = "B", GroupName = "Group A" });
                context.Projects.Add(new Project { Name = "C", GroupName = "Group A" });
                context.Projects.Add(new Project { Name = "D", GroupName = "Group B" });

                context.Instruments.Add(new Instrument { ID = "Instrument A", Projects = "A" });
                context.Instruments.Add(new Instrument { ID = "Instrument B", Projects = "B" });
                context.Instruments.Add(new Instrument { ID = "Instrument C", Projects = "C, D" });
                context.Instruments.Add(new Instrument { ID = "Instrument D", Projects = "D" });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var projectRepo = new ProjectRepository(context);
                var instrumentRepo = new InstrumentRepository(context);

                var repo = new InstrumentService(projectRepo, instrumentRepo);
                var result = repo.GetInstrumentIdRelateToProjectsOfGroup("Group A");
                Assert.Equal(3, result.Count);
            }
        }

        [Fact]
        public void GetInstrumentIdRelateToProjectsOfGroup_GroupHasNoProject_Should_ReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetInstrumentIdRelateToProjectsOfGroup_GroupHasNoProject_Should_ReturnEmpty))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Projects.Add(new Project { Name = "A", GroupName = "Group A" });
                context.Projects.Add(new Project { Name = "B", GroupName = "Group B" });

                context.Instruments.Add(new Instrument { ID = "Instrument A", Projects = "A" });
                context.Instruments.Add(new Instrument { ID = "Instrument B", Projects = "B" });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var projectRepo = new ProjectRepository(context);
                var instrumentRepo = new InstrumentRepository(context);

                var repo = new InstrumentService(projectRepo, instrumentRepo);
                var result = repo.GetInstrumentIdRelateToProjectsOfGroup("Group C");
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetInstrumentIdRelateToProjectsOfGroup_InstrumentHasNoProject_Should_ReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetInstrumentIdRelateToProjectsOfGroup_InstrumentHasNoProject_Should_ReturnEmpty))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Projects.Add(new Project { Name = "A", GroupName = "Group A" });

                context.Instruments.Add(new Instrument { ID = "Instrument A", });
                context.Instruments.Add(new Instrument { ID = "Instrument B", });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var projectRepo = new ProjectRepository(context);
                var instrumentRepo = new InstrumentRepository(context);

                var repo = new InstrumentService(projectRepo, instrumentRepo);
                var result = repo.GetInstrumentIdRelateToProjectsOfGroup("Group A");
                Assert.Empty(result);
            }
        }


        [Fact]
        public async Task GetToBeCalibateInstrument_PlanDateHasBeenExceedee_ShouldBeReturnSingle()
        {
            // 计划校准时间已过
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetToBeCalibateInstrument_PlanDateHasBeenExceedee_ShouldBeReturnSingle))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var calibrationCycle = 1;
                context.Instruments.Add(new Instrument { ID = "FXS-YZ01", CalibrationCycle = calibrationCycle, Group = "VD" });
                context.Calibrations.Add(new Calibration { InstrumentID = "FXS-YZ01", Date = System.DateTime.Now.AddYears(-calibrationCycle) });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var projectRepo = new ProjectRepository(context);
                var instrumentRepo = new InstrumentRepository(context);

                var repo = new InstrumentService(projectRepo, instrumentRepo);
                var result = await repo.GetToBeCalibateInstrument();
                Assert.Single(result);
            }
        }

        [Fact]
        public async Task GetToBeCalibateInstrument_29DaysLeftBeforePlanDate_ShouldBeReturnSingle()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetToBeCalibateInstrument_29DaysLeftBeforePlanDate_ShouldBeReturnSingle))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var calibrationCycle = 1;
                context.Instruments.Add(new Instrument { ID = "FXS-YZ01", CalibrationCycle = calibrationCycle, Group = "VD" });
                context.Calibrations.Add(new Calibration
                {
                    InstrumentID = "FXS-YZ01",
                    Date = System.DateTime.Now.AddYears(-calibrationCycle).AddDays(30) // 离计划校准时间还剩29天
                });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var projectRepo = new ProjectRepository(context);
                var instrumentRepo = new InstrumentRepository(context);

                var repo = new InstrumentService(projectRepo, instrumentRepo);
                var result = await repo.GetToBeCalibateInstrument();
                Assert.Single(result);
            }
        }

        [Fact]
        public async Task GetToBeCalibateInstrument_31DaysLeftBeforePlanDate_ShouldBeReturnEmpty()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetToBeCalibateInstrument_31DaysLeftBeforePlanDate_ShouldBeReturnEmpty))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var calibrationCycle = 1;
                context.Instruments.Add(new Instrument { ID = "FXS-YZ01", CalibrationCycle = calibrationCycle, Group = "VD" });
                context.Calibrations.Add(new Calibration
                {
                    InstrumentID = "FXS-YZ01",
                    Date = System.DateTime.Now.AddYears(-calibrationCycle).AddDays(32) // 离计划校准时间还剩31天
                });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var projectRepo = new ProjectRepository(context);
                var instrumentRepo = new InstrumentRepository(context);

                var repo = new InstrumentService(projectRepo, instrumentRepo);
                var result = await repo.GetToBeCalibateInstrument();
                Assert.Empty(result);
            }
        }
    }
}
