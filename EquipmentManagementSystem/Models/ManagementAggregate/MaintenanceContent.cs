using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MaintenanceContent
    {
        public int Id { get; set; }

        [Display(Name = "设备平台")]
        public string InstrumentPlatform { get; set; }

        [Display(Name = "维护类型")]
        public string Type { get; set; }

        [Display(Name = "维护内容")]
        [StringLength(100)]
        public string Text { get; set; }

        [Display(Name = "英文翻译")]
        [StringLength(100)]
        public string Translation { get; set; }

        [Display(Name = "维护周期(天)")]
        public int Cycle { get; set; }

        [Display(Name = "剩余天数提醒")]
        public int RemindTime { get; set; }
    }

    public class MaintenanceType
    {
        public static readonly string Daily = "日常维护";
        public static readonly string Weekly = "周维护";
        public static readonly string Monthly = "月度维护";
        public static readonly string Quarterly = "季度维护";
        public static readonly string HalfYearly = "半年维护";
        public static readonly string Yearly = "年度维护";
    }
}
