using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _equipmentContext;
        private readonly IMaintenanceRecordService _maintenanceRecordService;
        private readonly IInstrumentService _instrumentService;

        public IndexModel(EquipmentContext equipmentContext,
            IMaintenanceRecordService maintenanceRecordService, 
            IInstrumentService instrumentService)
        {
            _equipmentContext = equipmentContext;
            _instrumentService = instrumentService;
            _maintenanceRecordService = maintenanceRecordService;
        }

        // 主检设备数量
        public int InstrumentNumber { get; set; }

        // 待校准的主检设备
        public List<string> InstrumentOfExpire { get; set; }

        // 季度维护待维护
        public List<MaintenanceInfo> ToBeMaintainedOfQuarterly { get; set; }

        // 年度维护待维护
        public List<MaintenanceInfo> ToBeMaintainedOfYearly { get; set; }

        // 待跟进的故障工单
        public int MalfunctionWorkOrderOfFollowNumber { get; set; }

        // 故障
        public async Task OnGet()
        {
            InstrumentNumber = _equipmentContext.Instruments.Count();

            // 待校准设备
            InstrumentOfExpire = await _instrumentService.GetToBeCalibateInstrument();

            // 待维护设备
            ToBeMaintainedOfQuarterly = (await _maintenanceRecordService.GetToBeMaintenanceInfoOfQuarterly()).OrderBy(i => i.Group).ToList();
            ToBeMaintainedOfYearly = (await _maintenanceRecordService.GetToBeMaintenanceInfoOfYearly()).OrderBy(i => i.Group).ToList();

            // 待跟进工单
            MalfunctionWorkOrderOfFollowNumber = (from m in _equipmentContext.MalfunctionWorkOrder
                                                  where m.Progress != WorkOrderProgress.Completed
                                                  select m)
                                                  .Count();
        }

        public class MaintenanceInfo
        {
            [Display(Name = "项目组")]
            public string Group { get; set; }
            [Display(Name = "设备编号")]
            public string InstrumentId { get; set; }
            [Display(Name = "维护类型")]
            public string MaintenanceType { get; set; }
            [Display(Name = "维护内容")]
            public string MaintenanceContent { get; set; }
            [Display(Name = "上次维护时间")]
            [DataType(DataType.Date)]
            public DateTime MaintenanceTime { get; set; }
            public int cycle { get; set; }
            public int remindTime { get; set; }
            [Display(Name = "计划维护时间")]
            [DataType(DataType.Date)]
            public DateTime NextMaintenanceTime { get; set; }

            public override string ToString()
            {
                return $"{InstrumentId}-{MaintenanceType}-{MaintenanceContent}";
            }
        }
    }
}
