using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Assert
    {
        public int ID { get; set; }

        [Display(Name = "编号")]
        public string InstrumentID { get; set; }

        public Instrument Instrument { get; set; }

        [Display(Name = "编码")]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [Display(Name = "名称")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "入账日期")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [Required]
        [Display(Name = "来源单位")]
        [StringLength(50, MinimumLength = 1)]
        public string SourceUnit { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }
    }
}