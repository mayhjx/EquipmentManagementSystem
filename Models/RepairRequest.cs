using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 维修申请
    /// </summary>
    public class RepairRequest
    {
        [Display(Name = "报修时间")]
        public DateTime? RepairTime { get; set; }

        [Display(Name = "预约时间")]
        public DateTime? BookingsTime { get; set; }

        [Required]
        [Display(Name = "报修人")]
        public string Fixer { get; set; }

        [Required]
        [Display(Name = "对接工程师")]
        public string Engineer { get; set; }

        [Display(Name = "是否确认")]
        public bool IsConfirm { get; set; }
    }
}
