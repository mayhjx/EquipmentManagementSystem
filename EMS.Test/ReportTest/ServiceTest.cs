using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Repositories;
using EquipmentManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.ReportTest
{
    public class ServiceTest
    {
        [Theory]
        [InlineData(2020, 12, 9, 19)]
        [InlineData(2020, 6, 9, 18)]
        [InlineData(2020, 1, 9, 19)]
        [InlineData(2019, 12, 9, 17)]
        public void GetMonthlyUsageHoursOfInstrument(int year, int monthToInitial, int beginHour, int endHour)
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: $"{year}{monthToInitial}UsageHours")
                .Options;
            double expectTotal = 0;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var UsageRecords = new List<UsageRecord>();
                for (int month = 1; month <= monthToInitial; month++)
                {
                    var daysInMonth = DateTime.DaysInMonth(year, month);
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        expectTotal += endHour - beginHour;
                        UsageRecords.Add(new UsageRecord
                        {
                            InstrumentId = "FXS-YZ01",
                            BeginTime = new DateTime(year, month, day, beginHour, 00, 00),
                            EndTime = new DateTime(year, month, day, endHour, 00, 00)
                        });
                    }
                }

                context.AddRange(UsageRecords);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var usageRepo = new UsageRecordRepository(context);
                var malRepo = new MalfunctionRepository(context);
                var reportService = new ReportService(usageRepo, malRepo);
                var result = reportService.GetMonthlyUsageHoursOfInstrument("FXS-YZ01", year);
                Assert.Equal(12, result.Count);

                double total = 0;
                foreach (var hour in result)
                {
                    total += hour;
                }
                Assert.Equal(expectTotal, total);
            }
        }

        [Theory]
        [InlineData(2020, 12, 1, 9, 2, 19)]
        [InlineData(2020, 6, 1, 9, 2, 18)]
        [InlineData(2020, 1, 1, 9, 1, 19)]
        [InlineData(2019, 12, 1, 9, 1, 17)]
        public void GetMonthlyMalfunctionHoursOfInstrument(int year, int monthToInitial, int beginDay, int beginHour, int endDay, int endHour)
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase(databaseName: $"{year}{monthToInitial}MalfunctionHours")
                .Options;
            double expectTotal = 0;

            // Insert seed data into the database using one instance of the context
            using (var context = Utilities.CreateContext(options))
            {
                var MalfunctionWorkOrders = new List<MalfunctionWorkOrder>();
                for (int month = 1; month <= monthToInitial; month++)
                {
                    var beginTime = new DateTime(year, month, beginDay, beginHour, 00, 00);
                    var endTime = new DateTime(year, month, endDay, endHour, 00, 00);
                    expectTotal += Math.Round((endTime - beginTime).TotalHours, 1);

                    MalfunctionWorkOrders.Add(new MalfunctionWorkOrder
                    {
                        InstrumentID = "FXS-YZ01",
                        MalfunctionInfo = new MalfunctionInfo { BeginTime = beginTime },
                        Repair = new Repair { EndTime = endTime }
                    });
                }

                context.AddRange(MalfunctionWorkOrders);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = Utilities.CreateContext(options))
            {
                var usageRepo = new UsageRecordRepository(context);
                var malRepo = new MalfunctionRepository(context);
                var reportService = new ReportService(usageRepo, malRepo);
                var result = reportService.GetMonthlyMalfunctionHoursOfInstrument("FXS-YZ01", year);
                Assert.Equal(12, result.Count);

                double total = 0;
                foreach (var hour in result)
                {
                    total += hour;
                }
                Assert.Equal(expectTotal, total);
            }
        }
    }
}
