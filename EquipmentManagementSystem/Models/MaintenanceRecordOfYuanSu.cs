using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Models
{
    public class MaintenanceRecordOfYuanSu
    {
        public int Id { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "项目组")]
        public string GroupName { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "日维护")]
        public string Daily { get; set; }

        [Display(Name = "月维护")]
        public string Monthly { get; set; }

        [Display(Name = "半年维护")]
        public string HalfYearly { get; set; }

        [Display(Name = "临时维护")]
        public string Temporary { get; set; }

        public void SetDaily(string[] content)
        {
            Daily = string.Join(",", content);
        }

        public List<string> GetDaily()
        {
            return Daily?.Split(",").ToList();
        }

        public void SetMonthly(string[] content)
        {
            Monthly = string.Join(",", content);
        }

        public void SetHalfYearly(string[] content)
        {
            HalfYearly = string.Join(",", content);
        }

        public List<string> GetHalfYearly()
        {
            return HalfYearly?.Split(",").ToList();
        }

        public void SetTemporary(string[] content)
        {
            Temporary = string.Join(",", content);
        }

        public List<string> GetTemporary()
        {
            return Temporary?.Split(",").ToList();
        }
    }
}
