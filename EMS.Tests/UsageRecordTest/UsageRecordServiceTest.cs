using EMS.Infrastructure;
using EMS.Core.Entities.UsageRecordAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EMS.Infrastructure.Repositories;
using EMS.Tests;

namespace tests
{
    public class UsageRecordServiceTest
    {
        //[Fact]
        //public async Task Test_GetLastestRecordByProjectAndInstrumentAsync()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    // Arrange
        //    var expectProjectName = "VD";
        //    var expectInstrument = "FXS-YZ01";
        //    var seedingData = seeding_Test_GetLastestRecordByProjectAndInstrumentAsync();
        //    await db.AddRangeAsync(seedingData);
        //    await db.SaveChangesAsync();

        //    var expect = seedingData.FindLast(d => d.ProjectName == expectProjectName && d.InstrumentNumber== expectInstrument);
        //    var service = new GenericRepository<UsageRecord>(db);

        //    // Act
        //    var actual = await service.GetLastestRecordByProjectAndInstrumentAsync(expectProjectName, expectInstrument);

        //    // Assert
        //    Assert.Equal(expect, actual);
        //}

        //public List<UsageRecord> seeding_Test_GetLastestRecordByProjectAndInstrumentAsync()
        //{
        //    return new List<UsageRecord>
        //    {
        //        new UsageRecord{
        //            BeginTime=new DateTime(2020,11,01,08,30,00),
        //            EndTime=new DateTime(2020,11,01,18,00,00),
        //            InstrumentNumber = "FXS-YZ01",
        //            GroupName = "25OHD",
        //            ProjectName="VD",
        //            User="Test User"
        //        },
        //        new UsageRecord{
        //            BeginTime=new DateTime(2020,11,02,08,30,00),
        //            EndTime=new DateTime(2020,11,02,18,00,00),
        //            InstrumentNumber = "FXS-YZ01",
        //            GroupName = "25OHD",
        //            ProjectName="VD",
        //            User="Test User"
        //        },
        //        new UsageRecord{
        //            BeginTime=new DateTime(2020,11,03,08,30,00),
        //            EndTime=new DateTime(2020,11,03,18,00,00),
        //            InstrumentNumber = "FXS-YZ01",
        //            GroupName = "25OHD",
        //            ProjectName="VD",
        //            User="Test User"
        //        },
        //        new UsageRecord{
        //            BeginTime=new DateTime(2020,11,03,08,30,00),
        //            EndTime=new DateTime(2020,11,03,18,00,00),
        //            InstrumentNumber = "FXS-YZ01",
        //            GroupName = "ZH",
        //            ProjectName="CAS",
        //            User="Test User"
        //        },
        //        new UsageRecord{
        //            BeginTime=new DateTime(2020,11,03,08,30,00),
        //            EndTime=new DateTime(2020,11,03,18,00,00),
        //            InstrumentNumber = "FXS-YZ02",
        //            GroupName = "25OHD",
        //            ProjectName="VD",
        //            User="Test User"
        //        },
        //    };
        //}

        //[Fact]
        //public async Task Test_GetLastNColumnPressureAsync()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var seeding = seeding_Test_GetLastNColumnPressure();
        //    await db.AddRangeAsync(seeding);
        //    await db.SaveChangesAsync();

        //    var service = new GenericRepository<UsageRecord>(db);
        //    var lastNColumnPressure = service.GetLastNColumnPressure("Test", "Test");

        //    Assert.Equal(10, lastNColumnPressure["A"].Count);
        //    Assert.Equal(0, lastNColumnPressure["A"].First());
        //    Assert.Equal(9, lastNColumnPressure["A"].Last());
        //    Assert.Equal(10, lastNColumnPressure["B"].Count);
        //    Assert.Equal(1, lastNColumnPressure["B"].First());
        //    Assert.Equal(10, lastNColumnPressure["B"].Last());
        //}

        //public List<UsageRecord> seeding_Test_GetLastNColumnPressure()
        //{
        //    var result = new List<UsageRecord>() { };
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var column = "[{\"System\":\"A\",\"SerialNumber\":\"001\",\"Value\":" + i + ",\"Unit\":\"Mpa\"},{\"System\":\"B\",\"SerialNumber\":\"002\",\"Value\":" + (i + 1) + ",\"Unit\":\"Mpa\"}]";
        //        result.Add(new UsageRecord { Column = column, ProjectName = "Test", InstrumentNumber = "Test" });
        //    }
        //    return result;
        //}

        //[Fact]
        //public async Task Test_GetLastNColumnPressureAsync_NotTestData()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var seeding = new UsageRecord() { User = "Test User", ProjectName = "Test", InstrumentNumber = "Test" };
        //    await db.AddRangeAsync(seeding);
        //    await db.SaveChangesAsync();

        //    var service = new GenericRepository<UsageRecord>(db);
        //    var lastNColumnPressure = service.GetLastNColumnPressure("Test", "Test");

        //    Assert.Empty(lastNColumnPressure);
        //}

        //[Fact]
        //public async Task Test_GetLastNTestAsync()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var seeding = seeding_Test_GetLastNTest();
        //    await db.AddRangeAsync(seeding);
        //    await db.SaveChangesAsync();

        //    var service = new GenericRepository<UsageRecord>(db);
        //    var lastNTest = service.GetLastNTest("Test", "Test");

        //    Assert.Equal(10, lastNTest["S1"].Count);
        //    Assert.Equal(0, lastNTest["S1"].First());
        //    Assert.Equal(9, lastNTest["S1"].Last());
        //    Assert.Equal(10, lastNTest["S2"].Count);
        //    Assert.Equal(1, lastNTest["S2"].First());
        //    Assert.Equal(10, lastNTest["S2"].Last());
        //}

        //public List<UsageRecord> seeding_Test_GetLastNTest()
        //{
        //    var result = new List<UsageRecord>() { };
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var test = "[{\"System\":\"S1\",\"Value\":" + i + ",\"Unit\":\"cps\"},{\"System\":\"S2\",\"Value\":" + (i + 1) + ",\"Unit\":\"cps\"}]";
        //        result.Add(new UsageRecord { Test = test, ProjectName = "Test", InstrumentNumber = "Test" });
        //    }
        //    return result;
        //}

        //[Fact]
        //public async Task Test_GetLastNTestAsync_NotTestData()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var seeding = new UsageRecord() { User = "Test User", ProjectName = "Test", InstrumentNumber = "Test" };
        //    await db.AddRangeAsync(seeding);
        //    await db.SaveChangesAsync();

        //    var service = new GenericRepository<UsageRecord>(db);
        //    var lastNTest = service.GetLastNTest("Test", "Test");

        //    Assert.Empty(lastNTest);
        //}

        //[Fact]
        //public async Task Test_GetLastNVacuumDegreeAsync()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var seeding = seeding_Test_GetLastNVacuumDegree();
        //    await db.AddRangeAsync(seeding);
        //    await db.SaveChangesAsync();

        //    var service = new GenericRepository<UsageRecord>(db);
        //    var lastNVacuumDegree = service.GetLastNVacuumDegree("Test", "Test");

        //    Assert.Equal(10, lastNVacuumDegree["High"].Count);
        //    Assert.Equal(100, lastNVacuumDegree["High"].First());
        //    Assert.Equal(109, lastNVacuumDegree["High"].Last());
        //    Assert.Equal(10, lastNVacuumDegree["UlterHigh"].Count);
        //    Assert.Equal(101, lastNVacuumDegree["UlterHigh"].First());
        //    Assert.Equal(110, lastNVacuumDegree["UlterHigh"].Last());
        //}

        //public List<UsageRecord> seeding_Test_GetLastNVacuumDegree()
        //{
        //    var result = new List<UsageRecord>() { };
        //    for (int i = 100; i < 110; i++)
        //    {
        //        var vacuum = "[{\"System\":\"High\",\"Value\":" + i + ",\"Unit\":\"torr\"},{\"System\":\"UlterHigh\",\"Value\":" + (i + 1) + ",\"Unit\":\"torr\"}]";
        //        result.Add(new UsageRecord { VacuumDegree = vacuum, ProjectName = "Test", InstrumentNumber = "Test" });
        //    }
        //    return result;
        //}

        //[Fact]
        //public async Task Test_GetLastNVacuumDegreeAsync_NotVacuumDegreeData()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var seeding = new UsageRecord() { User = "Test User", ProjectName = "Test", InstrumentNumber = "Test" };
        //    await db.AddAsync(seeding);
        //    await db.SaveChangesAsync();
        //    var service = new GenericRepository<UsageRecord>(db);

        //    var lastNVacuumDegree = service.GetLastNVacuumDegree("Test", "Test");

        //    Assert.Empty(lastNVacuumDegree);
        //}

        //[Fact]
        //public void Test_GetLastN_NotAnyData()
        //{
        //    using var db = new ApplicationContext(Utilities.TestDbContextOptions());
        //    var service = new GenericRepository<UsageRecord>(db);

        //    var lastNColumnPressure = service.GetLastNColumnPressure("Test", "Test");
        //    var lastNTest = service.GetLastNTest("Test", "Test");
        //    var lastNVacuumDegree = service.GetLastNVacuumDegree("Test", "Test");

        //    Assert.Empty(lastNColumnPressure);
        //    Assert.Empty(lastNTest);
        //    Assert.Empty(lastNVacuumDegree);
        //}

    }
}
