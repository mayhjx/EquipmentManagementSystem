using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionReason
    {
        public int ID { get; set; }

        [Display(Name = "可能原因")]
        public string Reason { get; set; }
    }
}
