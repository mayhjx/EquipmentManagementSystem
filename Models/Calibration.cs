using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public enum Result
    {
        [Display(Name="不合格")]
        Failed, 
        [Display(Name="合格")]
        Passed
    }
    public class Calibration
    {
        public int ID { get; set; }

        [Display(Name="设备编号")]
        public string InstrumentID { get; set; }

        [Display(Name="执行时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name="结果")]
        public Result Result { get; set; }

        [Required]
        [Display(Name="检定/校准单位")]
        public string Unit { get; set; }

        [Required]
        [Display(Name="经办人")]
        public string Calibrator { get; set; }

        public Instrument instrument { get;set; }
    }
}