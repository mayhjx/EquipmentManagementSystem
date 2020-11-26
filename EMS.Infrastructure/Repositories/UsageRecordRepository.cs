using EMS.Core.Entities.UsageRecordAggregate;
using EMS.Core.Interfaces;
using EMS.Infrastructure;
using EMS.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class UsageRecordRepository : GenericRepository<UsageRecord>, IUsageRecordRepository
    {
        public UsageRecordRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<UsageRecord> GetAllByInstrument(string instrumentNumber)
        {
            return context.Set<UsageRecord>().Where(r => r.InstrumentNumber == instrumentNumber);
        }

        public IEnumerable<UsageRecord> GetAllByProject(string projectName)
        {
            return context.Set<UsageRecord>().Where(r => r.ProjectName == projectName);
        }

        public IEnumerable<UsageRecord> GetAllByGroup(string groupName)
        {
            return context.Set<UsageRecord>().Where(r => r.GroupName == groupName);
        }
    }
}
