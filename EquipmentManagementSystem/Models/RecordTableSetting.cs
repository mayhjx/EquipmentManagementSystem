using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Models
{
    public class RecordTableSetting
    {
        public int Id { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "使用记录表中文标题")]
        public string UsageRecordTableChineseTitle { get; set; }

        [Display(Name = "使用记录表英文标题")]
        public string UsageRecordTableEnglishTitle { get; set; }

        [Display(Name = "使用记录表表号")]
        public string UsageRecordTableNumber { get; set; }

        [Display(Name = "维护记录表中文标题")]
        public string MaintenanceRecordTableChineseTitle { get; set; }

        [Display(Name = "维护记录表英文标题")]
        public string MaintenanceRecordTableEnglishTitle { get; set; }

        [Display(Name = "维护记录表表号")]
        public string MaintenanceRecordTableNumber { get; set; }
    }
}
