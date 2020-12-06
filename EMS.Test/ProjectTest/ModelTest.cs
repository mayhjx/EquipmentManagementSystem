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

        [Fact]
        public void SetIonSource_AndGet_ShouldBeSame()
        {
            List<string> ionSources = new List<string> { "A", "B", "C" };
            var project = new Project();

            project.SetIonSource(ionSources);

            Assert.Equal(ionSources, project.GetIonSource());
        }

        [Fact]
        public void SetDetector_AndGet_ShouldBeSame()
        {
            List<string> detectors = new List<string> { "A", "B", "C" };
            var project = new Project();

            project.SetDetector(detectors);

            Assert.Equal(detectors, project.GetDetector());
        }

        [Fact]
        public void SetMobilePhase_AndGet_ShouldRemoveEmptyElement()
        {
            List<string> mobilePhases = new List<string> { "A", "B", "" };
            var project = new Project();

            project.SetMobilePhase(mobilePhases);

            Assert.Equal(new List<string> { "A", "B" }, project.GetMobilePhase());
        }

        [Fact]
        public void SetColumnType_AndGet_ShouldRemoveEmptyElement()
        {
            List<string> columnTypes = new List<string> { "A", "B", "" };
            var project = new Project();

            project.SetColumnType(columnTypes);

            Assert.Equal(new List<string> { "A", "B" }, project.GetColumnType());
        }

        [Fact]
        public void SetIonSource_AndGet_ShouldRemoveEmptyElement()
        {
            List<string> ionSources = new List<string> { "A", "B", "" };
            var project = new Project();

            project.SetIonSource(ionSources);

            Assert.Equal(new List<string> { "A", "B" }, project.GetIonSource());
        }

        [Fact]
        public void SetDetector_AndGet_ShouldRemoveEmptyElement()
        {
            List<string> detectors = new List<string> { "A", "B", "" };
            var project = new Project();

            project.SetDetector(detectors);

            Assert.Equal(new List<string> { "A", "B" }, project.GetDetector());
        }
    }
}
