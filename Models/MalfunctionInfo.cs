using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障信息
    /// </summary>
    public class MalfunctionInfo
    {
        public int ID { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        public DateTime BeginTime { get; set; }

        [Display(Name = "发现时间")]
        [DataType(DataType.DateTime)]
        public DateTime FoundedTime { get; set; }

        [Display(Name = "故障类型")]
        public MalfunctionType Type { get; set; }

        [Display(Name = "故障部件")]
        public string Part { get; set; }

        [Display(Name = "故障现象")]
        public string Phenomenon { get; set; }

        [Display(Name = "可能原因")]
        public string Reason { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "附件")]
        public string Attachment { get; set; }

        [Display(Name = "是否确认")]
        public bool IsConfirm { get; set; }
    }

    public enum MalfunctionType
    {
        [Display(Name = "硬件")]
        Hardware,
        [Display(Name = "软件")]
        Software,
        [Display(Name = "信号")]
        Signal
    }
}
