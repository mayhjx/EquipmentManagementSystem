using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test
{
    public class GenericRepositoryTest
    {
        [Fact]
        public void GetById_WithARecord_ShouldBe_ReturnTheRecord()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "GetById_WithARecord_ShouldBe_ReturnTheRecord")
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord { InstrumentID = "FXS-YZ01" });
                context.UsageRecords.Add(new UsageRecord { InstrumentID = "FXS-YZ02" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new GenericRepository<UsageRecord>(context);
                var result = repo.GetById(1).Result;
                Assert.Equal("FXS-YZ01", result.InstrumentID);
            }
        }

        [Fact]
        public void GetById_WithNoRecord_ShouldBe_Null()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "GetById_WithNoRecord_ShouldBe_Null")
                .Options;

            // Use a clean instance of the context to run the test
            // 如果是UsageRecord Model 的话，运行后返回一个UsageRecord实例，测试未通过，是因为内存数据库的名称相同导致的，改成不同的名称后运行通过。
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new GenericRepository<UsageRecord>(context);
                var result = repo.GetById(1).Result;
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetAll_ShouldBe_Three()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_ShouldBe_Three")
                .Options;

            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(new UsageRecord());
                context.UsageRecords.Add(new UsageRecord());
                context.UsageRecords.Add(new UsageRecord());
                context.SaveChanges();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new GenericRepository<UsageRecord>(context);
                var records = await repo.GetAll();
                Assert.Equal(3, records.Count);
            }
        }

        [Fact]
        public async Task CreateOneRecord()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "CreateOneRecord")
                .Options;

            var usageRecord = new UsageRecord { InstrumentID = "FXS-YZ02" };

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new GenericRepository<UsageRecord>(context);
                await repo.Create(usageRecord);
            }

            using (var context = Utilities.CreateContext(options))
            {
                var result = context.UsageRecords.Find(1);
                var records = await context.UsageRecords.ToListAsync();
                Assert.Equal("FXS-YZ02", result.InstrumentID);
                Assert.Single(records);
            }
        }

        [Fact]
        public async Task UpdateARecord()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "UpdateARecord")
                .Options;

            var usageRecord = new UsageRecord { InstrumentID = "FXS-YZ02" };


            using (var context = Utilities.CreateContext(options))
            {
                context.UsageRecords.Add(usageRecord);
                await context.SaveChangesAsync();
            }

            using (var context = Utilities.CreateContext(options))
            {
                var repo = new GenericRepository<UsageRecord>(context);

                var usageRecordToUpdate = await repo.GetById(1);
                usageRecordToUpdate.ProjectName = "Test";
                await repo.Update(usageRecordToUpdate);
            }

            using (var context = Utilities.CreateContext(options))
            {
                var usageRecordAfterUpdate = context.UsageRecords.Find(1);
                Assert.Equal(usageRecord.InstrumentID, usageRecordAfterUpdate.InstrumentID);
                Assert.Equal("Test", usageRecordAfterUpdate.ProjectName);
            }
        }

        [Fact]
        public async Task Delete_ShouldBe_Empty()
        {
            var option = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "Delete_ShouldBe_Empty")
                .Options;

            using (var context = Utilities.CreateContext(option))
            {
                context.UsageRecords.Add(new UsageRecord { });
                context.SaveChanges();
                var records = await context.UsageRecords.ToListAsync();
                Assert.Single(records);
            }

            using (var context = Utilities.CreateContext(option))
            {
                var repo = new GenericRepository<UsageRecord>(context);
                var usageRecordToDelete = await repo.GetById(1);
                await repo.Delete(usageRecordToDelete);
            }

            using (var context = Utilities.CreateContext(option))
            {
                var records = await context.UsageRecords.ToListAsync();
                Assert.Empty(records);
            }
        }
    }
}
