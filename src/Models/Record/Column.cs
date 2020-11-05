using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Models.Record
{
    [NotMapped]
    public class Column
    {
        public string System { get; set; }
        public string SerialNumber { get; set; }
        public float Value { get; set; }
        public string Unit { get; set; }
    }
}
