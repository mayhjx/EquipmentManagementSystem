using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class ProjectTeam
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="组名")]
        [StringLength(10, MinimumLength=1)]
        public string Name { get; set; }

        public ICollection<Project> projects { get; set; }

        public ICollection<Instrument> instruments { get; set; }
    }
}