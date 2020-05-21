using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionPhenomenon
    {
        public int ID { get; set; }

        [Display(Name = "故障现象")]
        public string Phenomenon { get; set; }
    }
}
