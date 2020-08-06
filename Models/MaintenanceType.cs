using System.Collections.Generic;
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

        [Display(Name = "维护内容")]
        public ICollection<MaintenanceContent> Content { get; set; }

        [Display(Name = "提醒周期(天)")]
        public int Cycle { get; set; }

        [Display(Name = "提醒时间")]
        public int RemindTime { get; set; }
    }
}
