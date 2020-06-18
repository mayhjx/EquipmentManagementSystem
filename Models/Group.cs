using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Group
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Display(Name = "组名")]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
