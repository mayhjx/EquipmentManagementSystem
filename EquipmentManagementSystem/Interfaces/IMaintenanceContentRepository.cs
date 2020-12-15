﻿using EquipmentManagementSystem.Models;
using System.Collections.Generic;
namespace EquipmentManagementSystem.Interfaces
{
    public interface IMaintenanceContentRepository : IGenericRepository<MaintenanceContent>
    {
        public List<MaintenanceContent> GetByInstrumentPlatform(string platform);
        public List<string> GetDailyContentByInstrumentPlatform(string platform);
        public List<string> GetWeeklyContentByInstrumentPlatform(string platform);
    }
}
