using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 性能验证
    /// </summary>
    public class Validation
    {
        public int ID { get; set; }

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "验证时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "故障修复后性能验证报告")]
        public string PerformanceReportFile { get; set; }

        [Display(Name = "故障前病人结果评估报告")]
        public string EffectReportFile { get; set; }

        [Display(Name = "总结分析")]
        public string Summary { get; set; }

        [Display(Name = "附件")]
        public string Attachment { get; set; }

        [Display(Name = "是否确认")]
        public bool IsConfirm { get; set; }
    }
}
