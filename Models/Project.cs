using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(50, MinimumLength=1)]
        public string Name { get; set; }

        public string instrumentId { get; set; }
        public Instrument instrument { get; set; }
        
    }
}