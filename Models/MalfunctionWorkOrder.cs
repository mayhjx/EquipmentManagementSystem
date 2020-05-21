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
        public string Progress { get; set; }

        [Required]
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

    //public enum MalfunctionStatus
    //{
    //    [Display(Name = "跟进中")]
    //    FollowUp,
    //    [Display(Name = "已完成")]
    //    Finished
    //}

}