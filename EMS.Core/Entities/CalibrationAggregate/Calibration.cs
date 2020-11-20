using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities.CalibrationAggregate
{
    public class Calibration : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "设备编号")]
        public string InstrumentNumber { get; set; }
        public int InstrumentId { get; set; }
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
        [StringLength(10)]
        [Display(Name = "检定/校验结果")]
        public string Result { get; set; }

        [Display(Name = "校准报告")]
        public CalibrationUploadFile UploadFile { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "经办人")]
        public string Operator { get; set; }
    }
}
