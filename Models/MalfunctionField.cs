using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionField
    {
        public int ID { get; set; }

        [Display(Name = "部位")]
        public string Name { get; set; }

        [Display(Name = "部件")]
        public ICollection<MalfunctionPart> MalfunctionParts { get; set; }

        [Display(Name = "故障信息")]
        public Malfunction Malfunction { get; set; }
    }
}
