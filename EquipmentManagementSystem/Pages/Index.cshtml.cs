using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EquipmentManagementSystem.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _equipmentContext;

        public IndexModel(EquipmentContext context)
        {
            _equipmentContext = context;
        }

        // 主检设备数量
        public int InstrumentNumber { get; set; }

        // 待校准的主检设备
        public List<Instrument> InstrumentOfExpire { get; set; }

        // 待维护的主检设备
        public List<MaintenanceInfo> InstrumentToBeMaintained { get; set; }

        // 待跟进的故障工单
        public int MalfunctionWorkOrderOfFollowNumber { get; set; }

        // 故障
        public void OnGet()
        {
            InstrumentNumber = _equipmentContext.Instruments.Count();

            // 待校准设备
            InstrumentOfExpire = (from m in _equipmentContext.Instruments
                                .AsNoTracking()
                                .Include(m => m.Calibrations)
                                .AsEnumerable()
                                  where (m.Status == "正常")
                                  where (m.Calibrations.Count > 0 && m.Calibrations.Last().Date != DateTime.MinValue)
                                  let remainDay = m.Calibrations.Last().Date.AddYears(m.CalibrationCycle) - DateTime.Today
                                  where remainDay.Days < 30 // 到期前30天内
                                  select m)
                                .ToList();


            // 分平台获取不同维护内容的维护周期和提醒时间
            var maintenanceCycleOfPlatform = new Dictionary<string, Dictionary<string, int>>();
            var maintenanceRemindTimeOfPlatform = new Dictionary<string, Dictionary<string, int>>();

            foreach (var item in _equipmentContext.MaintenanceContents)
            {
                var platForm = item.InstrumentPlatform;
                var content = item.Text;
                var cycle = item.Cycle;

                if (!maintenanceCycleOfPlatform.ContainsKey(platForm))
                {
                    maintenanceCycleOfPlatform.Add(platForm, new Dictionary<string, int>());
                }
                if (!maintenanceCycleOfPlatform[platForm].ContainsKey(content))
                {
                    maintenanceCycleOfPlatform[platForm].Add(content, cycle);
                }
                else
                {
                    maintenanceCycleOfPlatform[platForm].Add(content + "_1", cycle);
                }
            }

            foreach (var item in _equipmentContext.MaintenanceContents)
            {
                var platForm = item.InstrumentPlatform;
                var content = item.Text;
                var remindTime = item.RemindTime;

                if (!maintenanceRemindTimeOfPlatform.ContainsKey(platForm))
                {
                    maintenanceRemindTimeOfPlatform.Add(platForm, new Dictionary<string, int>());
                }
                if (!maintenanceRemindTimeOfPlatform[platForm].ContainsKey(content))
                {
                    maintenanceRemindTimeOfPlatform[platForm].Add(content, remindTime);
                }
                else
                {
                    maintenanceRemindTimeOfPlatform[platForm].Add(content + "_1", remindTime);

                }
            }

            // 将合并的维护内容拆分成一条条记录
            var maintenanceInfo = (from record in _equipmentContext.MaintenanceRecords
                                    .AsNoTracking()
                                    .Include(m => m.Instrument)
                                    .OrderBy(record => record.InstrumentId)
                                    .OrderByDescending(record => record.BeginTime) // 时间倒序排列
                                    .AsEnumerable()
                                   where record.Type != "临时维护" && record.Type != "日常维护" // 排除临时维护和日常维护
                                   let platFrom = record.Instrument.Platform
                                   let contents = record.Content.Split(", ")
                                   // 拆分维护内容
                                   from content in contents
                                   let cycle = maintenanceCycleOfPlatform[platFrom][content]
                                   let remindTime = maintenanceRemindTimeOfPlatform[platFrom][content]
                                   let lastMaintenanceDay = record.EndTime.GetValueOrDefault() // 上次维护时间
                                   where lastMaintenanceDay != DateTime.MinValue // 排除无结束时间的记录
                                   select new MaintenanceInfo
                                   {
                                       InstrumentId = record.InstrumentId,
                                       MaintenanceType = record.Type,
                                       MaintenanceContent = content,
                                       MaintenanceTime = lastMaintenanceDay,
                                       cycle = cycle,
                                       remindTime = remindTime,
                                       NextMaintenanceTime = lastMaintenanceDay.AddDays(cycle)
                                   })
                                    .ToList();

            // 获取最新的维护记录
            maintenanceInfo = RemoveDuplicatesRecord(maintenanceInfo);

            // 遍历维护记录，如果 下次维护周期 - 倒数提醒天数 <= 今天 则提示需要维护
            InstrumentToBeMaintained = (from record in maintenanceInfo
                                        where record.NextMaintenanceTime.AddDays(-record.remindTime) <= DateTime.Today
                                        select record)
                                        .ToList();

            // 待跟进工单
            MalfunctionWorkOrderOfFollowNumber = (from m in _equipmentContext.MalfunctionWorkOrder
                                                  where m.Progress != WorkOrderProgress.Completed
                                                  select m)
                                                  .Count();
        }

        // 删除重复的维护内容记录，只保留第一条
        public static List<T> RemoveDuplicatesRecord<T>(List<T> items)
        {
            // Use HashSet to maintain table of duplicates encountered.
            var result = new List<T>();
            var set = new HashSet<string>();
            for (int i = 0; i < items.Count; i++)
            {
                // If not duplicate, add to result.
                if (!set.Contains(items[i].ToString()))
                {
                    result.Add(items[i]);
                    // Record as a future duplicate.
                    set.Add(items[i].ToString());
                }
            }
            return result;
        }

        public class MaintenanceInfo
        {
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
            [Display(Name = "下次维护时间")]
            [DataType(DataType.Date)]
            public DateTime NextMaintenanceTime { get; set; }

            public override string ToString()
            {
                return $"{InstrumentId}-{MaintenanceType}-{MaintenanceContent}";
            }
        }
    }
}
