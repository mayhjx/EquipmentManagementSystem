using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentManagementSystem.Repositories
{
    public class GroupRepository:GenericRepository<Group>,IGroupRepository
    {
        public GroupRepository(EquipmentContext context):base(context)
        { }
    }
}
