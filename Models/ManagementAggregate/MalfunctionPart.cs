using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionPart
    {
        public int ID { get; set; }

        [Display(Name = "故障部件")]
        public string Name { get; set; }
    }
}
