using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "名称")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "仪器编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "所属项目组")]
        public ProjectTeam ProjectTeam { get; set; }

    }
}