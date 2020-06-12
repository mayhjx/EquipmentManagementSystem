using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class UsageRecord
    {
        public int Id { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        public DateTime BeginTime { get; set; }

        [Display(Name = "结束时间")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Display(Name = "样品数量")]
        public int SampleNumber { get; set; }

        [Display(Name = "单个样品检测时间")]
        [DataType(DataType.Time)]
        public DateTime TestTime { get; set; }

        [Display(Name = "仪器编号")]
        public string InstrumentId { get; set; }

        public Instrument Instrument { get; set; }

        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }

        public Project Project { get; set; }
    }
}
