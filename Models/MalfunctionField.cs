using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class MalfunctionField
    {
        public int ID { get; set; }

        [Display(Name = "部位")]
        public string Name { get; set; }

        [Display(Name = "问题/现象")]
        public ICollection<MalfunctionProblem> Problem { get; set; }
    }
}
