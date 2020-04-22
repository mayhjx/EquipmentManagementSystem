using System;
using System.ComponentModel.DataAnnotations;


namespace EquipmentManagementSystem.Models
{
    public class MalfunctionInvestigation
    {
        public int ID { get; set; }

        [Display(Name = "排查开始时间")]
        public DateTime BeginTime { get; set; }

        [Display(Name = "排查结束时间")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "排查人员")]
        public string Staff { get; set; }

        [Display(Name = "措施")]
        public string Measures { get; set; }
    }
}
