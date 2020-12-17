using System.Collections.Generic;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IInstrumentService
    {
        /// <summary>
        /// 返回项目组相关的检测仪器
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        List<string> GetInstrumentIdRelateToProjectsOfGroup(string group);
    }
}
