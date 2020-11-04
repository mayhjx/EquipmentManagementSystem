using System.Collections.Generic;
using EquipmentManagementSystem.Models.Record;
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
                new Column() {Type="A", SerialNumber="001", Value=1f, Unit="Mpa" },
            };

            var usageRecord = new UsageRecord();
            usageRecord.SetColumnInfo(columns);
            var actual = usageRecord.GetColumnInfo();

            Assert.Equal(usageRecord.Column, "[{\"Type\":\"A\",\"SerialNumber\":\"001\",\"Value\":1.0,\"Unit\":\"Mpa\"}]");
            Assert.Equal(columns[0].Type, actual[0].Type);
            Assert.Equal(columns[0].SerialNumber, actual[0].SerialNumber);
            Assert.Equal(columns[0].Value, actual[0].Value);
            Assert.Equal(columns[0].Unit, actual[0].Unit);
        }

        [Fact]
        public void Test_SetAndGetVacuumDegreeInfo()
        {
            var vacuumDegrees = new List<VacuumDegree>
            {
                new VacuumDegree() {Type="IG", Value=1f, Unit="torr" },
            };

            var usageRecord = new UsageRecord();
            usageRecord.SetVacuumDegreeInfo(vacuumDegrees);
            var actual = usageRecord.GetVacuumDegreeInfo();

            Assert.Equal(vacuumDegrees[0].Type, actual[0].Type);
            Assert.Equal(vacuumDegrees[0].Value, actual[0].Value);
            Assert.Equal(vacuumDegrees[0].Unit, actual[0].Unit);
        }

        [Fact]
        public void Test_SetAndGetTestInfo()
        {
            var tests = new List<Test>
            {
                new Test() {System="S1", Value=1f, Unit="" },
            };

            var usageRecord = new UsageRecord();
            usageRecord.SetTestInfo(tests);
            var actual = usageRecord.GetTestInfo();

            Assert.Equal(tests[0].System, actual[0].System);
            Assert.Equal(tests[0].Value, actual[0].Value);
            Assert.Equal(tests[0].Unit, actual[0].Unit);
        }
    }
}
