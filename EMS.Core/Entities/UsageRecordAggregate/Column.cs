using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Core.Entities.UsageRecordAggregate
{
    [NotMapped]
    public class Column
    {
        public string System { get; set; }
        public string SerialNumber { get; set; }
        public float Value { get; set; }
        public string Unit { get; set; }
    }
}
