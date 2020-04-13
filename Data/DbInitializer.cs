using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace EquipmentManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EquipmentContext context)
        {
            context.Database.EnsureCreated();

            InsertInstrument(context);
            InsertCalibration(context);

        }

        private static void InsertInstrument(EquipmentContext context)
        {   
            if (context.Instruments.Any())
            {
                return;   // DB has been seeded
            }

            string [] Datas = Reader(@"C:\Users\lihua\source\repos\EquipmentManagementSystem\wwwroot\Instruments.csv");

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;
                var data = line.Split(",");
                // 无启用日期转换
                DateTime datetime;
                DateTime.TryParse(data[3], out datetime); 
                
                context.Instruments.Add(
                    new Instrument{
                        ID=data[0],
                        Platform=data[1],
                        Name=data[2],
                        StartUsingDate=datetime,
                        CalibrationCycle=int.Parse(data[4]),
                        MetrologicalCharacteristics=data[5],
                        Status=Status.Using,
                        Location=data[7],
                        Principal=data[8],
                        NewSystemCode=data[9],
                        Remark=data[10]
                    }
                );
            }
            context.SaveChanges();
        }

        private static void InsertCalibration(EquipmentContext context)
        {
            if (context.Calibrations.Any())
            {
                return;   // DB has been seeded
            }

            string [] Datas = Reader(@"C:\Users\lihua\source\repos\EquipmentManagementSystem\wwwroot\Calibrations.csv");

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;
                var data = line.Split(",");
                // 无启用日期转换
                DateTime datetime;
                DateTime.TryParse(data[1], out datetime); 

                context.Calibrations.Add(
                    new Calibration{
                        InstrumentID=data[0],
                        Date=datetime,
                        Unit=data[2],
                        Result=Result.Passed,
                        Calibrator=data[4],

                    }
                );
            }
            context.SaveChanges();
        }

        private static string[] Reader(string filepath)
        {
            string[] text = {};
            if (string.IsNullOrEmpty(filepath) || !filepath.EndsWith(".csv"))
            {
                return text;
            }
            var sr = new StreamReader(filepath, Encoding.Default);
            text = sr.ReadToEnd().Split("\r\n");
            return text;
        }
    }
}