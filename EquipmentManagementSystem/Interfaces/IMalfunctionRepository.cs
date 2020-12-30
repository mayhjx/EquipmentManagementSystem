using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IMalfunctionRepository : IGenericRepository<MalfunctionWorkOrder>
    {
        double GetTotalMalfunctionTimeOfRecords(List<MalfunctionWorkOrder> workOrders);

        List<MalfunctionWorkOrder> GetAllByInstrumentIdAndMonthOfBeginTime(string instrumentId, DateTime date);
    }
}
