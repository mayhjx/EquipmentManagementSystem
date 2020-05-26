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
        public DateTime? BeginTime { get; set; }

        [Display(Name = "排查结束时间")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "排查人员")]
        public string Operator { get; set; }

        [Display(Name = "措施")]
        public string Measures { get; set; }
    }
}
