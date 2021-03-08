using EquipmentManagementSystem.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.MaintenanceRecordOfYuanSuTest
{
    public class ModelTest
    {
        [Fact]
        public void SetDaily_ThenGet_ShouldEqual()
        {
            string[] content = new string[] { "A", "B", "C" };
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetDaily(content);
            var result = maintenanceRecord.GetDaily();

            Assert.Equal(content, result);
        }

        [Fact]
        public void SetDaily_Empty_ShouldBeEmptyString()
        {
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetDaily(new string[] { });

            Assert.Equal("", maintenanceRecord.Daily);
        }

        //[Fact]
        //public void SetWeekly_ThenGet_ShouldEqual()
        //{
        //    string[] content = new string[] { "A", "B", "C" };
        //    var maintenanceRecord = new MaintenanceRecord();

        //    maintenanceRecord.SetWeekly(content);
        //    var result = maintenanceRecord.GetWeekly();

        //    Assert.Equal(content, result);
        //}

        //[Fact]
        //public void SetWeekly_Empty_ShouldBeEmptyString()
        //{
        //    var maintenanceRecord = new MaintenanceRecord();

        //    maintenanceRecord.SetWeekly(new string[] { });

        //    Assert.Equal("", maintenanceRecord.Weekly);
        //}

        [Fact]
        public void SetMonthly_ThenGet_ShouldEqual()
        {
            string[] content = new string[] { "A", "B", "C" };
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetMonthly(content);
            var result = maintenanceRecord.GetMonthly();

            Assert.Equal(content, result);
        }

        [Fact]
        public void SetMonthly_Empty_ShouldBeEmptyString()
        {
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetMonthly(new string[] { });

            Assert.Equal("", maintenanceRecord.Monthly);
        }

        //[Fact]
        //public void SetQuarterly_ThenGet_ShouldEqual()
        //{
        //    string[] content = new string[] { "A", "B", "C" };
        //    var maintenanceRecord = new MaintenanceRecordOfYuanSu();

        //    maintenanceRecord.SetQuarterly(content);
        //    var result = maintenanceRecord.GetQuarterly();

        //    Assert.Equal(content, result);
        //}

        //[Fact]
        //public void SetQuarterly_Empty_ShouldBeEmptyString()
        //{
        //    var maintenanceRecord = new MaintenanceRecordOfYuanSu();

        //    maintenanceRecord.SetQuarterly(new string[] { });

        //    Assert.Equal("", maintenanceRecord.Quarterly);
        //}

        [Fact]
        public void SetHalfYearly_ThenGet_ShouldEqual()
        {
            string[] content = new string[] { "A", "B", "C" };
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetHalfYearly(content);
            var result = maintenanceRecord.GetHalfYearly();

            Assert.Equal(content, result);
        }

        [Fact]
        public void SetHalfYearly_Empty_ShouldBeEmptyString()
        {
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetHalfYearly(new string[] { });

            Assert.Equal("", maintenanceRecord.HalfYearly);
        }

        //[Fact]
        //public void SetYearly_ThenGet_ShouldEqual()
        //{
        //    string[] content = new string[] { "A", "B", "C" };
        //    var maintenanceRecord = new MaintenanceRecordOfYuanSu();

        //    maintenanceRecord.SetYearly(content);
        //    var result = maintenanceRecord.GetYearly();

        //    Assert.Equal(content, result);
        //}

        //[Fact]
        //public void SetYearly_Empty_ShouldBeEmptyString()
        //{
        //    var maintenanceRecord = new MaintenanceRecordOfYuanSu();

        //    maintenanceRecord.SetYearly(new string[] { });

        //    Assert.Equal("", maintenanceRecord.Yearly);
        //}

        [Fact]
        public void SetTemporary_ThenGet_ShouldEqual()
        {
            string[] content = new string[] { "A", "B", "C" };
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetTemporary(content);
            var result = maintenanceRecord.GetTemporary();

            Assert.Equal(content, result);
        }

        [Fact]
        public void SetTemporary_Empty_ShouldBeEmptyString()
        {
            var maintenanceRecord = new MaintenanceRecordOfYuanSu();

            maintenanceRecord.SetTemporary(new string[] { });

            Assert.Equal("", maintenanceRecord.Temporary);
        }
    }
}
