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

        [Display(Name = "验证完成时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? FinishedTime { get; set; }

        [Display(Name = "性能验证报告")]
        [StringLength(100)]
        public string PerformanceReportFileName { get; set; }

        public byte[] PerformanceReportFile { get; set; }

        [Display(Name = "总结分析")]
        [StringLength(999)]
        public string Summary { get; set; }

        public byte[] Attachment { get; set; }

        [Display(Name = "附件")]
        [StringLength(100)]
        public string AttachmentName { get; set; }

        [Display(Name = "是否确认")]
        public bool IsConfirm { get; set; }
    }
}
