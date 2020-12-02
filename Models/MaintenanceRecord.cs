﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "项目名称")]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Display(Name = "项目ID")]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "维护类型")]
        [StringLength(20)]
        public string Type { get; set; }

        [Display(Name = "维护内容")]
        [StringLength(500)]
        public string Content { get; set; }

        [Display(Name = "操作者")]
        [StringLength(10)]
        public string Operator { get; set; }
    }
}