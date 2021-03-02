using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Models
{
    public class UsageRecordOfYuanSu
    {
        public int Id { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "项目组")]
        public string GroupName { get; set; }

        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTime { get; set; }

        [Display(Name = "灵敏度 Co(59)")]
        public double SensitivityCo { get; set; }
        [Display(Name = "灵敏度 Co RSD(%)")]
        public double SensitivityCoRSD { get; set; }
        [Display(Name = "灵敏度 Y(89)")]
        public double SensitivityY { get; set; }
        [Display(Name = "灵敏度 Y RSD(%)")]
        public double SensitivityYRSD { get; set; }
        [Display(Name = "灵敏度 Ti(205)")]
        public double SensitivityTi { get; set; }
        [Display(Name = "灵敏度 Ti RSD(%)")]
        public double SensitivityTiRSD { get; set; }

        [Display(Name = "分辨率 Co(59)")]
        public double MassResolutionCo { get; set; }
        [Display(Name = "分辨率 Y(89)")]
        public double MassResolutionY { get; set; }
        [Display(Name = "分辨率 Ti(205)")]
        public double MassResolutionTi { get; set; }

        [Display(Name = "质量轴 Co(59)")]
        public double MassAxisCo { get; set; }
        [Display(Name = "质量轴 Y(89)")]
        public double MassAxisY { get; set; }
        [Display(Name = "质量轴 Ti(205)")]
        public double MassAxisTi { get; set; }

        [Display(Name = "氧化物 CeO/Ce(%)")]
        public double OxideOfCe { get; set; }
        [Display(Name = "双电荷 Ce2+/Ce(%)")]
        public double DoubleChargeOfCe { get; set; }

        [Display(Name = "Mass 78")]
        public double Mass78 { get; set; }

        [Display(Name = "结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "操作者")]
        public string Operator { get; set; }

        [Display(Name = "备注")]
        [StringLength(20)]
        public string Remark { get; set; }
    }
}
