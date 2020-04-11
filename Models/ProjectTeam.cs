using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class ProjectTeam
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(10, MinimumLength=1)]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Instrument> instruments { get; set; }
    }
}