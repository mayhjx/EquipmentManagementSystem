using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 故障维修
    /// </summary>
    public class Maintenance
    {
        public int ID { get; set; }

        [Display(Name = "维修人")]
        public string Repairer { get; set; }

        [Display(Name = "解决措施")]
        public string Solution { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "附件")]
        public string Attachment { get; set; }

    }
}
