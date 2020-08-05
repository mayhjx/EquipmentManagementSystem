﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MaintenanceType
    {
        public int Id { get; set; }

        [Display(Name = "设备平台")]
        public string InstrumentPlatform { get; set; }

        [Display(Name = "维护类型")]
        public string Type { get; set; }

        [Display(Name = "维护内容（使用/分隔）")]
        public ICollection<MaintenanceContent> Content { get; set; }

        [Display(Name = "提醒周期")]
        public string Cycle { get; set; }

        [Display(Name = "提醒时间")]
        public string RemindTime { get; set; }
    }
}
