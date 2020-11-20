using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services;
using RazorPagesTestSample.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace tests
{
    public class UsageRecordServiceTest
    {
        [Fact]
        public async Task Test_AddAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var user = "Test User";
            var usageRecord = new UsageRecord() { Id = 1, User = user };
            var service = new UsageRecordService(db);

            await service.AddAsync(usageRecord);

            Assert.Equal(1, db.UsageRecords.Count());
            Assert.Equal(1, db.UsageRecords.Single().Id);
            Assert.Equal(user, db.UsageRecords.Single().User);
        }

        [Fact]
        public async Task Test_GetByIdAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var usageRecord = new UsageRecord() { Id = 1 };
            var service = new UsageRecordService(db);
            await service.AddAsync(usageRecord);

            var actual = service.GetByIdAsync(1);

            Assert.Equal(1, db.UsageRecords.Count());
            Assert.Equal(1, actual.Id);
        }

        [Fact]
        public async Task Test_UpdateAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var expectUser = "Expect User";
            var usageRecord = new UsageRecord() { Id = 1, User = "Test User" };
            var service = new UsageRecordService(db);
            await service.AddAsync(usageRecord);

            Assert.Equal(1, db.UsageRecords.Count());
            Assert.NotEqual(expectUser, db.UsageRecords.Single().User);

            // 不需要调用Update方法，就会自动更新值，为什么？
            usageRecord.User = expectUser;

            Assert.Equal(expectUser, db.UsageRecords.Single().User);
        }

        [Fact]
        public async Task Test_GetLastestRecordByProjectAndInstrumentAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            // Arrange
            var expectProjectName = "VD";
            var expectInstrument = "FXS-YZ01";
            var seedingData = seeding_Test_GetLastestRecordByProjectAndInstrumentAsync();
            await db.AddRangeAsync(seedingData);
            await db.SaveChangesAsync();

            var expect = seedingData.FindLast(d => d.ProjectName == expectProjectName && d.InstrumentId == expectInstrument);
            var service = new UsageRecordService(db);

            // Act
            var actual = await service.GetLastestRecordByProjectAndInstrumentAsync(expectProjectName, expectInstrument);

            // Assert
            Assert.Equal(expect, actual);
        }

        public List<UsageRecord> seeding_Test_GetLastestRecordByProjectAndInstrumentAsync()
        {
            return new List<UsageRecord>
            {
                new UsageRecord{
                    BeginTime=new DateTime(2020,11,01,08,30,00),
                    EndTime=new DateTime(2020,11,01,18,00,00),
                    InstrumentId = "FXS-YZ01",
                    GroupName = "25OHD",
                    ProjectName="VD",
                    User="Test User"
                },
                new UsageRecord{
                    BeginTime=new DateTime(2020,11,02,08,30,00),
                    EndTime=new DateTime(2020,11,02,18,00,00),
                    InstrumentId = "FXS-YZ01",
                    GroupName = "25OHD",
                    ProjectName="VD",
                    User="Test User"
                },
                new UsageRecord{
                    BeginTime=new DateTime(2020,11,03,08,30,00),
                    EndTime=new DateTime(2020,11,03,18,00,00),
                    InstrumentId = "FXS-YZ01",
                    GroupName = "25OHD",
                    ProjectName="VD",
                    User="Test User"
                },
                new UsageRecord{
                    BeginTime=new DateTime(2020,11,03,08,30,00),
                    EndTime=new DateTime(2020,11,03,18,00,00),
                    InstrumentId = "FXS-YZ01",
                    GroupName = "ZH",
                    ProjectName="CAS",
                    User="Test User"
                },
                new UsageRecord{
                    BeginTime=new DateTime(2020,11,03,08,30,00),
                    EndTime=new DateTime(2020,11,03,18,00,00),
                    InstrumentId = "FXS-YZ02",
                    GroupName = "25OHD",
                    ProjectName="VD",
                    User="Test User"
                },
            };
        }

        [Fact]
        public async Task Test_GetLastNColumnPressureAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var seeding = seeding_Test_GetLastNColumnPressure();
            await db.AddRangeAsync(seeding);
            await db.SaveChangesAsync();

            var service = new UsageRecordService(db);
            var lastNColumnPressure = service.GetLastNColumnPressure("Test", "Test");

            Assert.Equal(10, lastNColumnPressure["A"].Count);
            Assert.Equal(0, lastNColumnPressure["A"].First());
            Assert.Equal(9, lastNColumnPressure["A"].Last());
            Assert.Equal(10, lastNColumnPressure["B"].Count);
            Assert.Equal(1, lastNColumnPressure["B"].First());
            Assert.Equal(10, lastNColumnPressure["B"].Last());
        }

        public List<UsageRecord> seeding_Test_GetLastNColumnPressure()
        {
            var result = new List<UsageRecord>() { };
            for (int i = 0; i < 10; i++)
            {
                var column = "[{\"System\":\"A\",\"SerialNumber\":\"001\",\"Value\":" + i + ",\"Unit\":\"Mpa\"},{\"System\":\"B\",\"SerialNumber\":\"002\",\"Value\":" + (i + 1) + ",\"Unit\":\"Mpa\"}]";
                result.Add(new UsageRecord { Column = column, ProjectName = "Test", InstrumentId = "Test" });
            }
            return result;
        }

        [Fact]
        public async Task Test_GetLastNColumnPressureAsync_NotTestData()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var seeding = new UsageRecord() { User = "Test User", ProjectName = "Test", InstrumentId = "Test" };
            await db.AddRangeAsync(seeding);
            await db.SaveChangesAsync();

            var service = new UsageRecordService(db);
            var lastNColumnPressure = service.GetLastNColumnPressure("Test", "Test");

            Assert.Empty(lastNColumnPressure);
        }

        [Fact]
        public async Task Test_GetLastNTestAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var seeding = seeding_Test_GetLastNTest();
            await db.AddRangeAsync(seeding);
            await db.SaveChangesAsync();

            var service = new UsageRecordService(db);
            var lastNTest = service.GetLastNTest("Test", "Test");

            Assert.Equal(10, lastNTest["S1"].Count);
            Assert.Equal(0, lastNTest["S1"].First());
            Assert.Equal(9, lastNTest["S1"].Last());
            Assert.Equal(10, lastNTest["S2"].Count);
            Assert.Equal(1, lastNTest["S2"].First());
            Assert.Equal(10, lastNTest["S2"].Last());
        }

        public List<UsageRecord> seeding_Test_GetLastNTest()
        {
            var result = new List<UsageRecord>() { };
            for (int i = 0; i < 10; i++)
            {
                var test = "[{\"System\":\"S1\",\"Value\":" + i + ",\"Unit\":\"cps\"},{\"System\":\"S2\",\"Value\":" + (i + 1) + ",\"Unit\":\"cps\"}]";
                result.Add(new UsageRecord { Test = test, ProjectName = "Test", InstrumentId = "Test" });
            }
            return result;
        }

        [Fact]
        public async Task Test_GetLastNTestAsync_NotTestData()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var seeding = new UsageRecord() { User = "Test User", ProjectName = "Test", InstrumentId = "Test" };
            await db.AddRangeAsync(seeding);
            await db.SaveChangesAsync();

            var service = new UsageRecordService(db);
            var lastNTest = service.GetLastNTest("Test", "Test");

            Assert.Empty(lastNTest);
        }

        [Fact]
        public async Task Test_GetLastNVacuumDegreeAsync()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var seeding = seeding_Test_GetLastNVacuumDegree();
            await db.AddRangeAsync(seeding);
            await db.SaveChangesAsync();

            var service = new UsageRecordService(db);
            var lastNVacuumDegree = service.GetLastNVacuumDegree("Test", "Test");

            Assert.Equal(10, lastNVacuumDegree["High"].Count);
            Assert.Equal(100, lastNVacuumDegree["High"].First());
            Assert.Equal(109, lastNVacuumDegree["High"].Last());
            Assert.Equal(10, lastNVacuumDegree["UlterHigh"].Count);
            Assert.Equal(101, lastNVacuumDegree["UlterHigh"].First());
            Assert.Equal(110, lastNVacuumDegree["UlterHigh"].Last());
        }

        public List<UsageRecord> seeding_Test_GetLastNVacuumDegree()
        {
            var result = new List<UsageRecord>() { };
            for (int i = 100; i < 110; i++)
            {
                var vacuum = "[{\"System\":\"High\",\"Value\":" + i + ",\"Unit\":\"torr\"},{\"System\":\"UlterHigh\",\"Value\":" + (i + 1) + ",\"Unit\":\"torr\"}]";
                result.Add(new UsageRecord { VacuumDegree = vacuum, ProjectName = "Test", InstrumentId = "Test" });
            }
            return result;
        }

        [Fact]
        public async Task Test_GetLastNVacuumDegreeAsync_NotVacuumDegreeData()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var seeding = new UsageRecord() { User = "Test User", ProjectName = "Test", InstrumentId = "Test" };
            await db.AddAsync(seeding);
            await db.SaveChangesAsync();
            var service = new UsageRecordService(db);

            var lastNVacuumDegree = service.GetLastNVacuumDegree("Test", "Test");

            Assert.Empty(lastNVacuumDegree);
        }

        [Fact]
        public void Test_GetLastN_NotAnyData()
        {
            using var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService());
            var service = new UsageRecordService(db);

            var lastNColumnPressure = service.GetLastNColumnPressure("Test", "Test");
            var lastNTest = service.GetLastNTest("Test", "Test");
            var lastNVacuumDegree = service.GetLastNVacuumDegree("Test", "Test");

            Assert.Empty(lastNColumnPressure);
            Assert.Empty(lastNTest);
            Assert.Empty(lastNVacuumDegree);
        }

    }
}
