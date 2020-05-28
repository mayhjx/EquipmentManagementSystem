using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障工单
    /// </summary>
    public class MalfunctionWorkOrder
    {
        public int ID { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "进度")]
        public WorkOrderProgress Progress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "建单人")]
        public string Creator { get; set; }

        [Display(Name = "建单时间")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "故障信息")]
        public MalfunctionInfo MalfunctionInfo { get; set; }

        [Display(Name = "故障排查")]
        public Investigation Investigation { get; set; }

        [Display(Name = "维修申请")]
        public RepairRequest RepairRequest { get; set; }

        [Display(Name = "配件下单")]
        public AccessoriesOrder AccessoriesOrder { get; set; }

        [Display(Name = "故障维修")]
        public Maintenance Maintenance { get; set; }

        [Display(Name = "性能验证")]
        public Validation Validation { get; set; }
    }

    public enum WorkOrderProgress
    {
        [Display(Name = "排查中")]
        Investigating,
        [Display(Name = "已排查")]
        Investigated,
        [Display(Name = "已报修")]
        RepairRequested,
        [Display(Name = "等待配件")]
        Waiting,
        [Display(Name = "维修中")]
        Repairing,
        [Display(Name = "已验证")]
        Validated,
    }
}