using EquipmentManagementSystem.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
    }
}
