using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 维修申请
    /// </summary>
    public class RepairRequest
    {
        public int ID { get; set; }

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "报修时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? RequestTime { get; set; }

        [Display(Name = "预约时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BookingsTime { get; set; }

        [Display(Name = "报修人")]
        [StringLength(50)]
        public string Applicant { get; set; }

        [Display(Name = "对接工程师")]
        [StringLength(50)]
        public string Engineer { get; set; }

        [Display(Name = "是否确认")]
        public bool IsConfirm { get; set; }
    }
}
