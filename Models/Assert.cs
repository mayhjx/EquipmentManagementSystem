using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Assert
    {
        public int ID { get; set; }

        [Display(Name="编码")]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(50,MinimumLength=1)]
        public string Name { get; set; }

        [Required]
        [Display(Name="来源单位")]
        [StringLength(50,MinimumLength=1)]
        public string SourceUnit { get; set; }

        [Display(Name="备注")]
        public string Remark { get; set; }

        [Display(Name="编号")]
        public string instrumentId { get; set; }
        public Instrument instrument { get; set; }
    }
}