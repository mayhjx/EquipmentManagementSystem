using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障维修
    /// </summary>
    public class Maintenance
    {
        public int ID { get; set; }

        public int MalfunctionWorkOrderID { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [Display(Name = "维修人")]
        [StringLength(50)]
        public string Repairer { get; set; }

        [Display(Name = "解决措施")]
        [StringLength(999)]
        public string Solution { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }

        [Display(Name = "附件")]
        [StringLength(100)]
        public string Attachment { get; set; }

    }
}
