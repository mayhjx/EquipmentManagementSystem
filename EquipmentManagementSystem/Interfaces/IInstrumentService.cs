using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// 待校准设备，到期前30天提醒
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetToBeCalibateInstrument();
    }
}
