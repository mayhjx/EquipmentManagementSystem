using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class UsageRecord
    {
        public int Id { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "项目组")]
        public string GroupName { get; set; }

        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }

        [Display(Name = "进样开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "流动相/载气")]
        public string MobilePhase { get; set; }

        [Display(Name = "色谱柱类型")]
        public string ColumnType { get; set; }

        [Display(Name = "S1色谱柱编号")]
        public string SystemOneColumnNumber { get; set; }

        [Display(Name = "S1色谱柱压力")]
        public float? SystemOneColumnPressure { get; set; }

        [Display(Name = "压力单位")]
        public string SystemOneColumnPressureUnit { get; set; }

        [Display(Name = "S2色谱柱编号")]
        public string SystemTwoColumnNumber { get; set; }

        [Display(Name = "S2色谱柱压力")]
        public float? SystemTwoColumnPressure { get; set; }

        [Display(Name = "压力单位")]
        public string SystemTwoColumnPressureUnit { get; set; }

        [Display(Name = "临床样数量")]
        public int? SampleNumber { get; set; }

        [Display(Name = "S1序列样品数量")]
        public int? SystemOneBatchNumber { get; set; }

        [Display(Name = "S2序列样品数量")]
        public int? SystemTwoBatchNumber { get; set; }

        [Display(Name = "低真空")]
        public float? LowVacuumDegree { get; set; }

        [Display(Name = "低真空度单位")]
        public string LowVacuumDegreeUnit { get; set; }

        [Display(Name = "高真空")]
        public float? HighVacuumDegree { get; set; }

        [Display(Name = "高真空度单位")]
        public string HighVacuumDegreeUnit { get; set; }

        [Display(Name = "Blank信号")]
        public float? BlankSignal { get; set; }

        [Display(Name = "系统一Test信号")]
        public float? SystemOneTestSignal { get; set; }

        [Display(Name = "系统二Test信号")]
        public float? SystemTwoTestSignal { get; set; }

        [Display(Name = "离子源")]
        public string IonSource { get; set; }

        [Display(Name = "检测器")]
        public string Detector { get; set; }

        [Display(Name = "进样结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "操作者")]
        public string Operator { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }

        public bool IsDelete { get; set; }
    }
}
