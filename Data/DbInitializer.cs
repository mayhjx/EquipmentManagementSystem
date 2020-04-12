using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using System;
using System.Linq;

namespace EquipmentManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EquipmentContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Instruments.Any())
            {
                return;   // DB has been seeded
            }

            var instruments = new Instrument[]
            {
                new Instrument{ID="FXS-YZ02",
                            Platform="LCMS",
                            Name="双通道液相色谱-串联四级杆质谱联用仪",
                            StartUsingDate=DateTime.Parse("2011-8-1"),
                            CalibrationCycle=1,
                            MetrologicalCharacteristics="二级",
                            Status=0,
                            Location="301",
                            Principal="余木俊",
                            Remark="",
                            NewSystemCode="001142"},

                new Instrument{ID="FXS-YZ06",
                            Platform="LCMS",
                            Name="液相色谱-串联四级杆质谱联用仪",
                            StartUsingDate=DateTime.Parse("2013-7-1"),
                            CalibrationCycle=1,
                            MetrologicalCharacteristics="二级",
                            Status=0,
                            Location="301",
                            Principal="余木俊",
                            Remark="",
                            NewSystemCode="001146"},
                
                new Instrument{ID="FXS-YZ03",
                            Platform="LCMS",
                            Name="双通道液相色谱-串联四级杆质谱联用仪",
                            StartUsingDate=DateTime.Parse("2012-7-1"),
                            CalibrationCycle=1,
                            MetrologicalCharacteristics="二级",
                            Status=0,
                            Location="301",
                            Principal="李冰玲",
                            Remark="",
                            NewSystemCode="001142"},
            };
            foreach (Instrument s in instruments)
            {
                context.Instruments.Add(s);
            }
            context.SaveChanges();

            // var courses = new Course[]
            // {
            //     new Course{CourseID=1050,Title="Chemistry",Credits=3},
            //     new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            //     new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            //     new Course{CourseID=1045,Title="Calculus",Credits=4},
            //     new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            //     new Course{CourseID=2021,Title="Composition",Credits=3},
            //     new Course{CourseID=2042,Title="Literature",Credits=4}
            // };
            // foreach (Course c in courses)
            // {
            //     context.Courses.Add(c);
            // }
            // context.SaveChanges();

            // var enrollments = new Enrollment[]
            // {
            //     new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            //     new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            //     new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            //     new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            //     new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            //     new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            //     new Enrollment{StudentID=3,CourseID=1050},
            //     new Enrollment{StudentID=4,CourseID=1050},
            //     new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            //     new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            //     new Enrollment{StudentID=6,CourseID=1045},
            //     new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            // };
            // foreach (Enrollment e in enrollments)
            // {
            //     context.Enrollments.Add(e);
            // }
            // context.SaveChanges();
        }
    }
}