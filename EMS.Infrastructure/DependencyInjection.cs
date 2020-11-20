using EMS.Core.Interfaces;
using EMS.Infrastructure.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddTransient<IGroupRepository, GroupRepository>();
            //services.AddTransient<IProjectRepository, ProjectRepository>();
            //services.AddTransient<IInstrumentRepository, InstrumentRepository>();
            //services.AddTransient<IComponentRepository, ComponentRepository>();
            //services.AddTransient<ICalibrationRepository, CalibrationRepository>();
            //services.AddTransient<IMalfunctionRepository, MalfunctionRepository>();
            services.AddTransient<IUsageRecordRepository, UsageRecordRepository>();
        }
    }
}
