using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities.GroupAggregate
{
    public class Group : BaseEntity
    {
        [StringLength(100)]
        [Display(Name = "组名")]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Instrument> Instruments { get; set; }
    }
}
