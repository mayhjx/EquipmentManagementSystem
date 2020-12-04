using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.ProjectTest
{
    public class ModelTest
    {
        [Fact]
        public void SetMobilePhase_AndGet_ShouldBeSame()
        {
            List<string> mobilePhases = new List<string> { "A", "B" };
            var project = new Project();

            project.SetMobilePhase(mobilePhases);

            Assert.Equal(mobilePhases, project.GetMobilePhase());
        }

        [Fact]
        public void SetColumnType_AndGet_ShouldBeSame()
        {
            List<string> columnTypes = new List<string> { "A", "B", "C" };
            var project = new Project();

            project.SetColumnType(columnTypes);

            Assert.Equal(columnTypes, project.GetColumnType());
        }
    }
}
