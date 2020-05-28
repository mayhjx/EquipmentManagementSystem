using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public enum Result
    {
        [Display(Name = "合格")]
        Passed,
        [Display(Name = "不合格")]
        Failed
    }
    public class Calibration
    {
        public int ID { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }

        public Instrument Instrument { get; set; }

        [Display(Name = "检定/校验日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "检定/校准单位")]
        public string Unit { get; set; }

        [Required]
        [Display(Name = "结果")]
        public Result Result { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "经办人")]
        public string Calibrator { get; set; }

    }
}