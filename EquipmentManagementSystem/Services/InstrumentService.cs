using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IInstrumentRepository _instrumentRepository;

        public InstrumentService(IProjectRepository projectRepository, IInstrumentRepository instrumentRepository)
        {
            _projectRepository = projectRepository;
            _instrumentRepository = instrumentRepository;
        }


        public List<string> GetInstrumentIdRelateToProjectsOfGroup(string group)
        {
            var result = new List<string>();

            var projects = _projectRepository.GetByGroup(group);

            foreach (var project in projects)
            {
                var instruments = _instrumentRepository.GetAllInstrumentIdByProject(project);
                result.AddRange(instruments);
            }
            return result.Distinct().ToList();
        }

        public async Task<List<string>> GetToBeCalibateInstrument()
        {
            const int remindDay = 30;
            var toBeCalibrate = new List<string>();
            var instrumentIds = _instrumentRepository.GetAllInstrumentId();

            foreach(var id in instrumentIds)
            {
                var latestDate = (await _instrumentRepository.GetLatestCalibratedDateOfInstrument(id)).GetValueOrDefault();
                if (latestDate == System.DateTime.MinValue) continue; // 无校准日期

                var calibrateCycle = (await _instrumentRepository.GetById(id)).CalibrationCycle;
                var planCalibarateDate = latestDate.AddYears(calibrateCycle).Date; // 计划校准日期
                var today = System.DateTime.Now.Date;

                if(planCalibarateDate <= today || (planCalibarateDate - today).Days <= remindDay)
                {
                    var group = (await _instrumentRepository.GetById(id)).Group;
                    toBeCalibrate.Add($"{group}:{id}:{planCalibarateDate.ToShortDateString()}");
                }
            }

            return toBeCalibrate.OrderBy(i=>i).ToList();
        }
    }
}
