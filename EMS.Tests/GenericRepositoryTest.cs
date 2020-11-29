using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EMS.Infrastructure.Repositories;
using EMS.Infrastructure;
using EMS.Core.Entities.UsageRecordAggregate;
using System.Threading.Tasks;
using System.Linq;

namespace EMS.Tests
{
    public class GenericRepositoryTest
    {
        [Fact]
        public async Task Test_Create()
        {
            using var db = new ApplicationContext(Utilities.TestDbContextOptions());
            var user = "Test";
            var usageRecord = new UsageRecord() {User = user };
            var genericRepository = new GenericRepository<UsageRecord>(db);

            await genericRepository.Create(usageRecord);

            Assert.Equal(1, db.UsageRecords.Count());
            Assert.Equal(user, db.UsageRecords.Single().User);
        }

        [Fact]
        public async Task Test_Update()
        {
            using var db = new ApplicationContext(Utilities.TestDbContextOptions());
            db.Set<UsageRecord>().Add(new UsageRecord() { });
            db.SaveChanges();
            var genericRepository = new GenericRepository<UsageRecord>(db);

            var usageRecordToUpdate = db.Set<UsageRecord>().Single();
            usageRecordToUpdate.User = "Test";
            await genericRepository.Update(usageRecordToUpdate);

            Assert.Equal(1, db.UsageRecords.Count());
            Assert.Equal("Test", db.UsageRecords.Single().User);
        }

        [Fact]
        public async Task Test_Delete()
        {
            using var db = new ApplicationContext(Utilities.TestDbContextOptions());
            db.Set<UsageRecord>().Add(new UsageRecord() { });
            db.SaveChanges();
            var genericRepository = new GenericRepository<UsageRecord>(db);

            var usageRecordToDelete = db.Set<UsageRecord>().Single();
            await genericRepository.Delete(usageRecordToDelete);

            Assert.Equal(0, db.UsageRecords.Count());
        }

        [Fact]
        public async Task Test_GetById()
        {
            using var db = new ApplicationContext(Utilities.TestDbContextOptions());
            var user = "Test";
            db.Set<UsageRecord>().Add(new UsageRecord() { User = user });
            db.SaveChanges();
            var genericRepository = new GenericRepository<UsageRecord>(db);

            var usageRecord = await genericRepository.GetById(1);

            Assert.Equal(user, usageRecord.User);
        }

        [Fact]
        public void Test_GetAll()
        {
            using var db = new ApplicationContext(Utilities.TestDbContextOptions());
            db.Set<UsageRecord>().Add(new UsageRecord() { });
            db.Set<UsageRecord>().Add(new UsageRecord() { });
            db.SaveChanges();
            var genericRepository = new GenericRepository<UsageRecord>(db);

            var usageRecords = genericRepository.GetAll();

            Assert.Equal(2, usageRecords.Count());
        }

        [Fact]
        public void Test_Find()
        {
            var targetUser = "Test 1";
            using var db = new ApplicationContext(Utilities.TestDbContextOptions());
            db.Set<UsageRecord>().Add(new UsageRecord() { User = targetUser });
            db.Set<UsageRecord>().Add(new UsageRecord() { User = "Test 2" });
            db.SaveChanges();
            var genericRepository = new GenericRepository<UsageRecord>(db);

            var usageRecord = genericRepository.Find(i => i.User == targetUser);

            Assert.Equal(targetUser, usageRecord.Single().User);
        }
    }
}
