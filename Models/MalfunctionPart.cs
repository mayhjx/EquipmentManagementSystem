using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionPart
    {
        public int ID { get; set; }

        [Display(Name = "部件名称")]
        public string Name { get; set; }

        [Display(Name = "问题/现象")]
        public ICollection<MalfunctionProblem> Problem { get; set; }

        [Display(Name = "故障部位")]
        public int MalfunctionFieldID { get; set; }
        public MalfunctionField MalfunctionField { get; set; }
    }
}
