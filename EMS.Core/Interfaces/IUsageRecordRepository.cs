using EMS.Core.Entities.UsageRecordAggregate;
using System.Collections.Generic;

namespace EMS.Core.Interfaces
{
    public interface IUsageRecordRepository : IGenericRepository<UsageRecord>
    {
        IEnumerable<UsageRecord> GetAllByProject(string projectName);
        IEnumerable<UsageRecord> GetAllByInstrument(string instrumentNumber);
    }
}
