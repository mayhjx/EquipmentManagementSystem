using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionProblem
    {
        public int ID { get; set; }

        [Display(Name = "问题/描述")]
        public string Describe { get; set; }

        [Display(Name = "可能原因")]
        public ICollection<MalfunctionReason> Reasons { get; set; }

        [Display(Name = "故障部件")]
        public int MalfunctionPartID { get; set; }
        public MalfunctionPart MalfunctionPart { get; set; }
    }
}
