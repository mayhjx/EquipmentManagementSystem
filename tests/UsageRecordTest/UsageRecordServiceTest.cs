using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services;
using RazorPagesTestSample.Tests;
using Xunit;

namespace tests
{
    public class UsageRecordServiceTest
    {
        [Fact]
        public async Task Test_GetLastestRecordByProjectAndInstrumentAsync()
        {
            using (var db = new EquipmentContext(Utilities.TestDbContextOptions(), Utilities.MockIUserResolverService()))
            {
                // Arrange
                var expectProjectName = "VD";
                var expectInstrument = "FXS-YZ01";
                var seedingData = seedingUsageRecord();
                await db.AddRangeAsync(seedingData);
                await db.SaveChangesAsync();

                var expect = seedingData.FindLast(d => d.ProjectName == expectProjectName && d.InstrumentId == expectInstrument);
                var service = new UsageRecordService(db);

                // Act
                var actual = await service.GetLastestRecordByProjectAndInstrumentAsync(expectProjectName, expectInstrument);

                // Assert
                Assert.Equal(expect, actual);
            }
        }

        public async Task Test_()
        {

        }

        public List<UsageRecord> seedingUsageRecord()
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

    }
}
