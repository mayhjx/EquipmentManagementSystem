using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 性能验证
    /// </summary>
    public class Validation
    {
        [Display(Name = "故障结束时间")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "故障修复后性能验证报告")]
        public IFormFile PerformanceReport { get; set; }

        [Display(Name = "故障前病人结果评估报告")]
        public IFormFile EffectReport { get; set; }

        [Display(Name = "总结分析")]
        public string Summary { get; set; }

        [Display(Name = "附件")]
        public IFormFile Attachment { get; set; }

        [Display(Name = "是否确认")]
        public bool IsConfirm { get; set; }
    }
}
