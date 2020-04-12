using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Component
    {
        public int ID { get; set; }

        [Display(Name="仪器编号")]
        public string instrumentID { get; set;}
        public Instrument instrument { get; set; }

        [Required]
        [Display(Name="序列号")]
        [StringLength(50,MinimumLength=1)]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(50,MinimumLength=1)]
        public string Name { get; set; }

        [Required]
        [Display(Name="型号")]
        [StringLength(50,MinimumLength=1)]
        public string Model { get; set; }

        public ICollection<Malfunction> malfunctions { get; set; }
    }
}