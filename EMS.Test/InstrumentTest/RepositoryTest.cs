using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.InstrumentTest
{
    public class RepositoryTest
    {
        [Fact]
        public void GetTestProjectsById()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                context.Instruments.Add(new Instrument { ID = "Test", Projects = "A, B" });
                context.Instruments.Add(new Instrument { ID = "Other", Projects = "C, D" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var repo = new InstrumentRepository(context);
                var result = repo.GetTestProjectsById("Test");
                Assert.Equal(new List<string> { "A", "B" }, result);
            }
        }


    }
}
