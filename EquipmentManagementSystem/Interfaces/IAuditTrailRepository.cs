﻿using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IAuditTrailRepository
    {
        Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName, int? id = null, DateTime? yearOrMonth = null);
    }
}
