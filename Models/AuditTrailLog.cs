using System;

namespace EquipmentManagementSystem.Models
{
    public class AuditTrailLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime DateChanged { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string PrimaryKeyValue { get; set; }
        public string OriginalValue { get; set; }
        public string CurrentValue { get; set; }
    }
}
