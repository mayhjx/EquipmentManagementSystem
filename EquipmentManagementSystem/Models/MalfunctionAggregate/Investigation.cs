using System;
using System.ComponentModel.DataAnnotations;


namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障排查
    /// </summary>
    public class Investigation
    {
        public int ID { get; set; }

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "排查开始时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "排查结束时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "排查人员")]
        [StringLength(50)]
        public string Operator { get; set; }

        [Display(Name = "排查过程")]
        [StringLength(999)]
        public string Measures { get; set; }

        [Display(Name = "排查结论")]
        public InvestigationResult Result { get; set; }
    }

    public enum InvestigationResult
    {
        [Display(Name = "需外部维修")]
        External,
        [Display(Name = "可内部维修")]
        Internal

    }
}
