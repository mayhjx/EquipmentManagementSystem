using EMS.Core.Entities.UsageRecordAggregate;

namespace EMS.Core.Interfaces
{
    public interface IUsageRecordRepository : IGenericRepository<UsageRecord>
    {
        //IEnumerable<UsageRecord> GetAllByGroup(string gruopName);
        //IEnumerable<UsageRecord> GetAllByProject(string projectName);
        //IEnumerable<UsageRecord> GetAllByInstrument(string instrumentNumber);
    }
}
