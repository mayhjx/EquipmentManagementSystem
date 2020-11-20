using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities.GroupAggregate
{
    public class Project : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "项目名称")]
        public string Name { get; set; }

        [Display(Name = "所属项目组")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Display(Name = "检测仪器")]
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
    }
}
