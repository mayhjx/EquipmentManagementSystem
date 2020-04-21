using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public enum Fields
    {
        [Display(Name = "进样系统")]
        SamplingSystem,
        [Display(Name = "输液/载气系统")]
        InfusionSystem,
        [Display(Name = "分离系统")]
        SeparationSystem,
        [Display(Name = "真空系统")]
        VacuumSystem,
        [Display(Name = "离子源")]
        IonSource,
        [Display(Name = "质量分析器")]
        QualityAnalyzer,
        [Display(Name = "检测系统")]
        DetectionSystem,
        [Display(Name = "数据系统")]
        DataSystem
    }

    public class Malfunction
    {
        public int ID { get; set; }

        [Display(Name = "故障领域")]
        public Fields Field { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }

        public Instrument Instrument { get; set; }

        [Display(Name = "部件")]
        public int ComponentID { get; set; }

        public Component Component { get; set; }

        [Required]
        [Display(Name = "现象/问题")]
        [StringLength(50, MinimumLength = 1)]
        public string Problem { get; set; }

        [Required]
        [Display(Name = "可能原因")]
        [StringLength(50, MinimumLength = 1)]
        public string Reason { get; set; }

        [Display(Name = "发现时间")]
        [DataType(DataType.DateTime)]
        public DateTime FoundTime { get; set; }

        [Display(Name = "排查开始时间")]
        [DataType(DataType.DateTime)]
        public DateTime StartTrackTime { get; set; }

        [Display(Name = "报修时间")]
        public DateTime ReportTime { get; set; }

        [Required]
        [Display(Name = "跟进人")]
        [StringLength(10, MinimumLength = 1)]
        public string FollowUpPeople { get; set; }

        [Display(Name = "排除时间")]
        public DateTime DebuggingTime { get; set; }

        [Display(Name = "配件下单时间")]
        public DateTime PlaceOrderTime { get; set; }

        [Display(Name = "配件到达时间")]
        public DateTime AccessoriesArrivalTime { get; set; }

        [Display(Name = "工程师上门时间")]
        public DateTime EngineerArrivalTime { get; set; }

        [Display(Name = "解决措施")]
        public string Solutions { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }
    }


}