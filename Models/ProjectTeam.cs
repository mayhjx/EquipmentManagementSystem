using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class ProjectTeam
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "组名")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "检测项目")]
        public ICollection<Project> Projects { get; set; }

        [Display(Name = "仪器")]
        public ICollection<Instrument> Instruments { get; set; }
    }
}