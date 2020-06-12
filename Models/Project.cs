using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "项目名")]
        public string Name { get; set; }
    }
}
