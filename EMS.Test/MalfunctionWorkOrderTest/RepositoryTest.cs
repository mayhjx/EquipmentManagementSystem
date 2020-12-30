using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.MalfunctionWorkOrderTest
{
    public class RepositoryTest
    {
        [Fact]
        public async Task GetTotalMalfunctionTime_ShouldBeEqual_24()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
               .UseInMemoryDatabase(databaseName: nameof(GetTotalMalfunctionTime_ShouldBeEqual_24))
               .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var malfunctionInfo = new MalfunctionInfo { BeginTime = new DateTime(2020, 12, 01, 10, 0, 0) };
                var repair = new Repair { EndTime = new DateTime(2020, 12, 02, 10, 0, 0) };
                var workOrder = new MalfunctionWorkOrder { MalfunctionInfo = malfunctionInfo, Repair = repair };

                context.MalfunctionWorkOrder.Add(workOrder);
                await context.SaveChangesAsync();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MalfunctionRepository(context);
                var workOrder = (await repo.GetAll()).ToList();
                Assert.Single(workOrder);

                var result = repo.GetTotalMalfunctionTimeOfRecords(workOrder);
                Assert.Equal(24, result);
            }
        }

        [Fact]
        public async Task GetTotalMalfunctionTime_NoEndTime_ShouldBeEqual_0()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
               .UseInMemoryDatabase(databaseName: nameof(GetTotalMalfunctionTime_NoEndTime_ShouldBeEqual_0))
               .Options;

            using (var context = Utilities.CreateContext(options))
            {
                var malfunctionInfo = new MalfunctionInfo { BeginTime = new DateTime(2020, 12, 01, 10, 0, 0) };
                var repair = new Repair { EndTime = null };
                var workOrder = new MalfunctionWorkOrder { MalfunctionInfo = malfunctionInfo, Repair = repair };

                context.MalfunctionWorkOrder.Add(workOrder);
                await context.SaveChangesAsync();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MalfunctionRepository(context);
                var workOrder = (await repo.GetAll()).ToList();
                Assert.Single(workOrder);

                var result = repo.GetTotalMalfunctionTimeOfRecords(workOrder);
                Assert.Equal(0, result);
            }
        }

        [Fact]
        public void GetAllByInstrumentIdAndBeginTime_Should_ReturnThreeRecords()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_Should_ReturnThreeRecords))
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.MalfunctionWorkOrder.Add(new MalfunctionWorkOrder { InstrumentID = "FXS-YZ01", MalfunctionInfo = new MalfunctionInfo { BeginTime = new DateTime(2020, 01, 01) } });
                context.MalfunctionWorkOrder.Add(new MalfunctionWorkOrder { InstrumentID = "FXS-YZ01", MalfunctionInfo = new MalfunctionInfo { BeginTime = new DateTime(2020, 01, 02) } });
                context.MalfunctionWorkOrder.Add(new MalfunctionWorkOrder { InstrumentID = "FXS-YZ01", MalfunctionInfo = new MalfunctionInfo { BeginTime = new DateTime(2020, 01, 03) } });
                context.MalfunctionWorkOrder.Add(new MalfunctionWorkOrder { InstrumentID = "FXS-YZ02", MalfunctionInfo = new MalfunctionInfo { BeginTime = new DateTime(2020, 01, 01) } });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MalfunctionRepository(context);
                var result = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 01, 01));
                Assert.Equal(3, result.Count);
            }
        }

        [Fact]
        public void GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllByInstrumentIdAndBeginTime_NoData_Should_ReturnEmptyList))
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new MalfunctionRepository(context);
                var result = repo.GetAllByInstrumentIdAndMonthOfBeginTime("FXS-YZ01", new DateTime(2020, 01, 01));
                Assert.Empty(result);
            }
        }
    }
}
