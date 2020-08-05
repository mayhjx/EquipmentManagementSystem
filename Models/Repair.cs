using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障维修
    /// </summary>
    public class Repair
    {
        public int ID { get; set; }

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "维修开始时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "维修人")]
        [StringLength(50)]
        public string Repairer { get; set; }

        [Display(Name = "解决措施")]
        [StringLength(100)]
        public string Solution { get; set; }

        [Display(Name = "维修完成时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "是否关键部位")]
        public bool IsCritical { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }

        [Display(Name = "工程师服务报告")]
        [StringLength(500)]
        public string FileName { get; set; }

        [Display(Name = "文件路径")]
        [StringLength(1000)]
        public string FilePath { get; set; }

        [Display(Name = "上传时间")]
        public DateTime? UploadTime { get; set; }

    }
}
