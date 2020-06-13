using EquipmentManagementSystem.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EquipmentManagementSystem.Data
{
    public static class DbInitializer
    {
        private static string Path;
        public static void Initialize(EquipmentContext context, string wwwrootPath)
        {
            Path = wwwrootPath;
            context.Database.EnsureCreated();

            InsertInstrument(context, Path + "/Instruments.csv");
            InsertCalibration(context, Path + "/Calibrations.csv");
            InsertAssert(context, Path + "/Asserts.csv");
            InsertComponent(context, Path + "/Components.csv");
            //InsertMalfunctionField(context, Path + "/MalfunctionField.csv");
            //InsertMalfunctionPart(context, Path + "/MalfunctionParts.csv");
            //InsertMalfunctionProblem(context, Path + "/MalfunctionProblems.csv");
            //InsertMalfunctionReason(context, Path + "/MalfunctionReasons.csv");
        }

        private static void InsertInstrument(EquipmentContext context, string filepath)
        {
            if (context.Instruments.Any())
            {
                return;   // DB has been seeded
            }

            string[] Datas = Reader(filepath);

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;
                var data = line.Split(",");
                // 无日期转换
                DateTime.TryParse(data[3], out DateTime datetime);

                context.Instruments.Add(
                    new Instrument
                    {
                        ID = data[0],
                        Platform = data[1],
                        Name = data[2],
                        StartUsingDate = datetime,
                        CalibrationCycle = int.Parse(data[4]),
                        MetrologicalCharacteristics = data[5],
                        Status = InstrumentStatus.Using,
                        Location = data[7],
                        Principal = data[8],
                        NewSystemCode = data[9],
                        Group = data[10],
                        Projects = data[11],
                        Remark = data[12]
                    }
                );
            }
            context.SaveChanges();
        }

        private static void InsertCalibration(EquipmentContext context, string filepath)
        {
            if (context.Calibrations.Any())
            {
                return;   // DB has been seeded
            }

            string[] Datas = Reader(filepath);

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;
                var data = line.Split(",");
                // 无日期转换
                DateTime.TryParse(data[1], out DateTime datetime);

                context.Calibrations.Add(
                    new Calibration
                    {
                        InstrumentID = data[0],
                        Date = datetime,
                        Unit = data[2],
                        Result = Result.Passed,
                        Calibrator = data[4],
                    }
                );
            }
            context.SaveChanges();
        }

        private static void InsertAssert(EquipmentContext context, string filepath)
        {
            if (context.Asserts.Any())
            {
                return;   // DB has been seeded
            }

            string[] Datas = Reader(filepath);

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;
                var data = line.Split(",");
                // 无日期转换
                DateTime.TryParse(data[3], out DateTime datetime);

                context.Asserts.Add(
                    new Assert
                    {
                        InstrumentId = data[0],
                        Code = data[1],
                        Name = data[2],
                        EntryDate = datetime,
                        SourceUnit = data[4],
                        Remark = data[5]
                    }
                );
            }
            context.SaveChanges();
        }

        private static void InsertComponent(EquipmentContext context, string filepath)
        {
            if (context.Components.Any())
            {
                return;   // DB has been seeded
            }

            string[] Datas = Reader(filepath);

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;
                var data = line.Split(",");

                context.Components.Add(
                    new Component
                    {
                        InstrumentID = data[0],
                        SerialNumber = data[1],
                        Name = data[2],
                        Model = data[3],
                        Brand = data[4],
                    }
                );
            }
            context.SaveChanges();
        }

        //private static void InsertMalfunctionField(EquipmentContext context, string filepath)
        //{
        //    if (context.MalfunctionFields.Any())
        //    {
        //        return;   // DB has been seeded
        //    }

        //    string[] Datas = Reader(filepath);

        //    foreach (var line in Datas.Skip(1))
        //    {
        //        if (line.Trim() == "") continue;
        //        var data = line.Split(",");

        //        context.MalfunctionFields.Add(
        //            new MalfunctionField
        //            {
        //                Name = data[0],
        //            }
        //        );
        //    }
        //    context.SaveChanges();
        //}

        //private static void InsertMalfunctionPart(EquipmentContext context, string filepath)
        //{
        //    if (context.MalfunctionParts.Any())
        //    {
        //        return;   // DB has been seeded
        //    }

        //    string[] Datas = Reader(filepath);

        //    foreach (var line in Datas.Skip(1))
        //    {
        //        if (line.Trim() == "") continue;
        //        var data = line.Split(",");

        //        context.MalfunctionParts.Add(
        //            new MalfunctionPart
        //            {
        //                MalfunctionFieldID = int.Parse(data[0]),
        //                Name = data[1],
        //            }
        //        );
        //    }
        //    context.SaveChanges();
        //}

        //private static void InsertMalfunctionProblem(EquipmentContext context, string filepath)
        //{
        //    if (context.MalfunctionProblems.Any())
        //    {
        //        return;   // DB has been seeded
        //    }

        //    string[] Datas = Reader(filepath);

        //    foreach (var line in Datas.Skip(1))
        //    {
        //        if (line.Trim() == "") continue;
        //        var data = line.Split(",");

        //        context.MalfunctionProblems.Add(
        //            new MalfunctionProblem
        //            {
        //                MalfunctionPartID = int.Parse(data[0]),
        //                Describe = data[1],
        //            }
        //        );
        //    }
        //    context.SaveChanges();
        //}

        //private static void InsertMalfunctionReason(EquipmentContext context, string filepath)
        //{
        //    if (context.MalfunctionReasons.Any())
        //    {
        //        return;   // DB has been seeded
        //    }

        //    string[] Datas = Reader(filepath);

        //    foreach (var line in Datas.Skip(1))
        //    {
        //        if (line.Trim() == "") continue;
        //        var data = line.Split(",");

        //        context.MalfunctionReasons.Add(
        //            new MalfunctionReason
        //            {
        //                MalfunctionProblemID = int.Parse(data[0]),
        //                Reason = data[1],
        //            }
        //        );
        //    }
        //    context.SaveChanges();
        //}

        private static string[] Reader(string filepath)
        {
            string[] text = { };
            if (string.IsNullOrEmpty(filepath) || !filepath.EndsWith(".csv"))
            {
                return text;
            }
            using (var sr = new StreamReader(filepath, Encoding.Default))
            {
                text = sr.ReadToEnd().Split("\r\n");
            }
            return text;
        }
    }
}