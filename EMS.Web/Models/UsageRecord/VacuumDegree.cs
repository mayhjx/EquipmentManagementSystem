using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Models.Record
{
    [NotMapped]
    public class VacuumDegree
    {
        public string System { get; set; }
        public float Value { get; set; }
        public string Unit { get; set; }
    }
}
