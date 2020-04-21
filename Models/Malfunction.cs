using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public enum Fields
    {
        [Display(Name = "进样系统（自动进样器）")]
        SamplingSystem,
        [Display(Name = "输液系统（泵）")]
        InfusionSystem,
        [Display(Name = "分离系统（色谱柱）")]
        SeparationSystem,
        [Display(Name = "离子源")]
        IonSource,
        [Display(Name = "真空系统")]
        VacuumSystem,
        [Display(Name = "透镜系统")]
        LensSystem,
        [Display(Name = "质量分析器Q1")]
        QualityAnalyzerQ1,
        [Display(Name = "碰撞池")]
        CollisionPool,
        [Display(Name = "质量分析器Q3")]
        QualityAnalyzerQ3,
        [Display(Name = "检测器")]
        DetectionSystem,
        [Display(Name = "机械泵")]
        MechanicalPumps,
        [Display(Name = "分子涡轮泵")]
        MolecularTurbinePump,
        [Display(Name = "数据系统（软件、通讯）")]
        DataSystem
    }

    public class Malfunction
    {
        public int ID { get; set; }


        //[Display(Name = "故障领域")]
        //public ICollection<MalfunctionField> Field { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }

        public Instrument Instrument { get; set; }

        [Display(Name = "部位")]
        public string FieldName { get; set; }
        public ICollection<MalfunctionField> Field { get; set; }

        //[Required]
        //[Display(Name = "现象/问题")]
        //[StringLength(50, MinimumLength = 1)]
        //public string Problem { get; set; }

        //[Required]
        //[Display(Name = "可能原因")]
        //[StringLength(50, MinimumLength = 1)]
        //public string Reason { get; set; }

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