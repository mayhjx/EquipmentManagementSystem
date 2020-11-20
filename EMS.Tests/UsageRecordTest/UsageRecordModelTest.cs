using EquipmentManagementSystem.Models.Record;
using System;
using System.Collections.Generic;
using Xunit;

namespace tests
{
    public class UsageRecordModelTest
    {
        [Fact]
        public void Test_SetAndGetColumnInfo()
        {
            var columns = new List<Column>
            {
                new Column() {System="A", SerialNumber="001", Value=1f, Unit="Mpa" },
            };

            var usageRecord = new UsageRecord();
            usageRecord.SetColumnInfo(columns);
            var actual = usageRecord.GetColumnInfo();

            //Assert.Equal("[{\"System\":\"A\",\"SerialNumber\":\"001\",\"Value\":1.0,\"Unit\":\"Mpa\"}]", usageRecord.Column);
            Assert.Equal(columns[0].System, actual[0].System);
            Assert.Equal(columns[0].SerialNumber, actual[0].SerialNumber);
            Assert.Equal(columns[0].Value, actual[0].Value);
            Assert.Equal(columns[0].Unit, actual[0].Unit);
        }

        [Fact]
        public void Test_NotSet_GetColumnInfo_ReturnNull()
        {
            var usageRecord = new UsageRecord();
            var actual = usageRecord.GetColumnInfo();

            Assert.Null(actual);
        }

        [Fact]
        public void Test_SetAndGetVacuumDegreeInfo()
        {
            var vacuumDegrees = new List<VacuumDegree>
            {
                new VacuumDegree() {System="IG", Value=1f, Unit="torr" },
            };

            var usageRecord = new UsageRecord();
            usageRecord.SetVacuumDegreeInfo(vacuumDegrees);
            var actual = usageRecord.GetVacuumDegreeInfo();

            Assert.Equal(vacuumDegrees[0].System, actual[0].System);
            Assert.Equal(vacuumDegrees[0].Value, actual[0].Value);
            Assert.Equal(vacuumDegrees[0].Unit, actual[0].Unit);
        }

        [Fact]
        public void Test_NotSet_GetVacuumDegreeInfo_ReturnNull()
        {
            var usageRecord = new UsageRecord();
            var actual = usageRecord.GetVacuumDegreeInfo();

            Assert.Null(actual);
        }

        [Fact]
        public void Test_SetAndGetTestInfo()
        {
            var tests = new List<Test>
            {
                new Test() {System="S1", Value=1f, Unit="cps" },
            };

            var usageRecord = new UsageRecord();
            usageRecord.SetTestInfo(tests);
            var actual = usageRecord.GetTestInfo();

            Assert.Equal(tests[0].System, actual[0].System);
            Assert.Equal(tests[0].Value, actual[0].Value);
            Assert.Equal(tests[0].Unit, actual[0].Unit);
        }

        [Fact]
        public void Test_NotSet_GetTestInfo_ReturnNull()
        {
            var usageRecord = new UsageRecord();
            var actual = usageRecord.GetTestInfo();

            Assert.Null(actual);
        }

        [Fact]
        public void Test_UpdateEndTime()
        {
            var endTime = new DateTime(2020, 11, 01, 18, 00, 00);
            var usageRecord = new UsageRecord() { EndTime = new DateTime(2020, 11, 01, 17, 0, 0) };
            usageRecord.UpdateEndTime(endTime);

            var actual = usageRecord.EndTime;

            Assert.Equal(endTime, actual);
        }
    }
}
