using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class WorkOrder
    {
        [Display(Name = "工单编号")]
        public int ID { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "完成时间")]
        public DateTime FinishedTime { get; set; }

        [Display(Name = "故障编号")]
        public int MalfunctionID { get; set; }

        public Malfunction Malfunction { get; set; }

        [Display(Name = "状态")]
        public WorkOrderStatus Status { get; set; }
    }

    public enum WorkOrderStatus
    {
        [Display(Name = "跟进中")]
        FollowUp,
        [Display(Name = "已完成")]
        Completed,
    }
}
