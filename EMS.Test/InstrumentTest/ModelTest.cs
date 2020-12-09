using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.InstrumentTest
{
    public class ModelTest
    {
        [Fact]
        public void SetProjects_ThenGet_Shouldbe_Same()
        {
            List<string> projects = new List<string> { "A", "B", "C" };
            var Instrument = new Instrument();

            Instrument.SetProjects(projects);

            Assert.Equal(projects, Instrument.GetProjects());
        }

        [Fact]
        public void GetProjects_Null_ShouldReturnEmptyList()
        {
            var Instrument = new Instrument { };

            var projects = Instrument.GetProjects();

            Assert.Equal(new List<string> { }, projects);
        }

        [Fact]
        public void HasProjects_Shouldbe_True()
        {
            var Instrument = new Instrument { Projects = "A, B, C" };

            var target = "A";

            Assert.True(Instrument.HasProject(target));
        }

        [Fact]
        public void HasProjects_Shouldbe_False()
        {
            var Instrument = new Instrument { Projects = "A, B, C" };

            var target = "D";

            Assert.False(Instrument.HasProject(target));
        }

        [Fact]
        public void HasProjects_Null_Shouldbe_False()
        {
            var Instrument = new Instrument { Projects = null };

            var target = "A";

            Assert.False(Instrument.HasProject(target));
        }
    }
}
