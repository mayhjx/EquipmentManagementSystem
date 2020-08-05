using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MaintenanceContent
    {
        public int Id { get; set; }

        [Display(Name = "内容")]
        [StringLength(50)]
        public string Text { get; set; }

        [Display(Name = "维护类型")]
        public int MaintenanceTypeId { get; set; }

        public MaintenanceType MaintenanceType { get; set; }
    }
}
