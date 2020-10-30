using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionSolution
    {
        public int ID { get; set; }

        [Display(Name = "解决措施")]
        public string Solution { get; set; }
    }
}
