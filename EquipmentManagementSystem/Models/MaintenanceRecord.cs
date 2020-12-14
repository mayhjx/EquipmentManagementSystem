using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EquipmentManagementSystem.Models
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "项目组")]
        public string GroupName { get; set; }

        //[Display(Name = "项目名称")]
        //public string ProjectName { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "日常维护")]
        public string Daily { get; set; }

        [Display(Name = "周维护")]
        public string Weekly { get; set; }

        [Display(Name = "月度维护")]
        public string Monthly { get; set; }

        [Display(Name = "季度维护")]
        public string Quarterly { get; set; }

        [Display(Name = "年度维护")]
        public string Yearly { get; set; }

        [Display(Name = "临时维护")]
        public string Temporary { get; set; }

        [Display(Name = "其他维护")]
        public string Other { get; set; }

        [Display(Name = "操作者")]
        public string Operator { get; set; }

        public void SetDaily(string[] content)
        {
            Daily = string.Join(",", content);
        }

        public List<string> GetDaily()
        {
            return Daily.Split(",").ToList();
        }

        public void SetWeekly(string[] content)
        {
            Weekly = string.Join(",", content);
        }

        public List<string> GetWeekly()
        {
            return Weekly.Split(",").ToList();
        }

        public void SetMonthly(string[] content)
        {
            Monthly = string.Join(",", content);
        }

        public List<string> GetMonthly()
        {
            return Monthly.Split(",").ToList();
        }

        public void SetQuarterly(string[] content)
        {
            Quarterly = string.Join(",", content);
        }

        public List<string> GetQuarterly()
        {
            return Quarterly.Split(",").ToList();
        }

        public void SetYearly(string[] content)
        {
            Yearly = string.Join(",", content);
        }

        public List<string> GetYearly()
        {
            return Yearly.Split(",").ToList();
        }

        public void SetTemporary(string[] content)
        {
            Temporary = string.Join(",", content);
        }

        public List<string> GetTemporary()
        {
            return Temporary.Split(",").ToList();
        }

        public void SetOther(string content)
        {
            Other = content;
        }

        public string GetOther()
        {
            return Other;
        }
    }
}
