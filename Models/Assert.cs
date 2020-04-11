using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Assert
    {
        [Key]
        public Instrument instrumentID { get; set; }

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
        public Instrument instruments { get; set; }
    }
}