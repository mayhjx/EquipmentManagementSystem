using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.InstrumentTest
{
    public class ModelTest
    {
        [Fact]
        public void SetProjects()
        {
            List<string> projects = new List<string> { "A", "B", "C" };
            var Instrument = new Instrument();

            Instrument.SetProjects(projects);

            Assert.Equal("A, B, C", Instrument.Projects);
        }

        [Fact]
        public void GetProjects()
        {
            var Instrument = new Instrument { Projects = "A, B, C" };

            var projects = Instrument.GetProjects();

            Assert.Equal(new List<string> { "A", "B", "C" }, projects);
        }

        [Fact]
        public void HasProjects_True()
        {
            var Instrument = new Instrument { Projects = "A, B, C" };

            var target = "A";

            Assert.True(Instrument.HasProject(target));
        }

        [Fact]
        public void HasProjects_False()
        {
            var Instrument = new Instrument { Projects = "A, B, C" };

            var target = "D";

            Assert.False(Instrument.HasProject(target));
        }
    }
}
