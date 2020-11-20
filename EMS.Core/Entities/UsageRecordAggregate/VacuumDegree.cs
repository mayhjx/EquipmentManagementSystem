using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Core.Entities.UsageRecordAggregate
{
    [NotMapped]
    public class VacuumDegree
    {
        public string System { get; set; }
        public float Value { get; set; }
        public string Unit { get; set; }
    }
}
