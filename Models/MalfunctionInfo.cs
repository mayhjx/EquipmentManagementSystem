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

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "故障开始时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime BeginTime { get; set; }

        [Display(Name = "故障发现时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime FoundedTime { get; set; }

        [Display(Name = "故障类型")]
        public MalfunctionType Type { get; set; }

        [Display(Name = "故障部件")]
        [StringLength(50)]
        public string Part { get; set; }

        [Display(Name = "故障现象")]
        [StringLength(50)]
        public string Phenomenon { get; set; }

        [Display(Name = "可能原因")]
        [StringLength(50)]
        public string Reason { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }

        [Display(Name = "附件")]
        [StringLength(500)]
        public string FileName { get; set; }

        [Display(Name = "文件路径")]
        [StringLength(1000)]
        public string FilePath { get; set; }

        [Display(Name = "上传时间")]
        public DateTime? UploadTime { get; set; }

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
