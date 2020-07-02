﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障维修
    /// </summary>
    public class Maintenance
    {
        public int ID { get; set; }

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "开始时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "维修人")]
        [StringLength(50)]
        public string Repairer { get; set; }

        [Display(Name = "解决措施")]
        [StringLength(100)]
        public string Solution { get; set; }

        [Display(Name = "完成时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "是否关键部位")]
        public bool IsCritical { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }

        public byte[] Attachment { get; set; }

        [Display(Name = "工程师服务报告")]
        [StringLength(100)]
        public string FileName { get; set; }

        [Display(Name = "上传时间")]
        public DateTime? UploadTime { get; set; }

    }
}
