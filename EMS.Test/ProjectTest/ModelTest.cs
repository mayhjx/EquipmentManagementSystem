using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.ProjectTest
{
    public class ModelTest
    {
        [Fact]
        public void GetMobilePhase_Null_ShouldBeReturnEmptyList()
        {
            var project = new Project();

            var result = project.GetMobilePhase();

            Assert.Equal(new List<string>(), result);
        }

        [Fact]
        public void SetMobilePhase_ShouldBeSame()
        {
            List<string> mobilePhases = new List<string> { "A", "B" };
            var project = new Project();

            project.SetMobilePhase(mobilePhases);

            Assert.Equal(mobilePhases, project.GetMobilePhase());
        }

        [Fact]
        public void SetMobilePhase_ShouldRemoveEmptyElement()
        {
            List<string> mobilePhases = new List<string> { "A", "B", "" };
            var project = new Project();

            project.SetMobilePhase(mobilePhases);

            Assert.Equal(new List<string> { "A", "B" }, project.GetMobilePhase());
        }

        //[Fact]
        //public void GetColumnType_Null_ShouldBeReturnEmptyList()
        //{
        //    var project = new Project();

        //    var result = project.GetColumnType();

        //    Assert.Equal(new List<string>(), result);
        //}

        //[Fact]
        //public void SetColumnType_ShouldBeSame()
        //{
        //    List<string> columnTypes = new List<string> { "A", "B", "C" };
        //    var project = new Project();

        //    project.SetColumnType(columnTypes);

        //    Assert.Equal(columnTypes, project.GetColumnType());
        //}

        //[Fact]
        //public void GetIonSource_Null_ShouldBeReturnEmptyList()
        //{
        //    var project = new Project();

        //    var result = project.GetIonSource();

        //    Assert.Equal(new List<string>(), result);
        //}

        //[Fact]
        //public void SetIonSource_ShouldBeSame()
        //{
        //    List<string> ionSources = new List<string> { "A", "B", "C" };
        //    var project = new Project();

        //    project.SetIonSource(ionSources);

        //    Assert.Equal(ionSources, project.GetIonSource());
        //}

        //[Fact]
        //public void GetDetector_Null_ShouldBeReturnEmptyList()
        //{
        //    var project = new Project();

        //    var result = project.GetDetector();

        //    Assert.Equal(new List<string>(), result);
        //}


        //[Fact]
        //public void SetDetector_ShouldBeSame()
        //{
        //    List<string> detectors = new List<string> { "A", "B", "C" };
        //    var project = new Project();

        //    project.SetDetector(detectors);

        //    Assert.Equal(detectors, project.GetDetector());
        //}


        //[Fact]
        //public void SetColumnType_ShouldRemoveEmptyElement()
        //{
        //    List<string> columnTypes = new List<string> { "A", "B", "" };
        //    var project = new Project();

        //    project.SetColumnType(columnTypes);

        //    Assert.Equal(new List<string> { "A", "B" }, project.GetColumnType());
        //}

        //[Fact]
        //public void SetIonSource_ShouldRemoveEmptyElement()
        //{
        //    List<string> ionSources = new List<string> { "A", "B", "" };
        //    var project = new Project();

        //    project.SetIonSource(ionSources);

        //    Assert.Equal(new List<string> { "A", "B" }, project.GetIonSource());
        //}

        //[Fact]
        //public void SetDetector_ShouldRemoveEmptyElement()
        //{
        //    List<string> detectors = new List<string> { "A", "B", "" };
        //    var project = new Project();

        //    project.SetDetector(detectors);

        //    Assert.Equal(new List<string> { "A", "B" }, project.GetDetector());
        //}
    }
}
