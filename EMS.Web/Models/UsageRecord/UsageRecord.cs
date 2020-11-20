using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models.Record
{
    public class UsageRecord : BaseEntity
    {
        [Display(Name = "设备编号")]
        public string InstrumentId { get; set; }

        [Display(Name = "项目组名称")]
        public string GroupName { get; set; }

        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime BeginTime { get; set; }

        [Display(Name = "流动相")]
        [StringLength(1000)]
        public string MobilePhase { get; set; }

        [Display(Name = "色谱柱类型")]
        [StringLength(1000)]
        public string ColumnType { get; set; }

        [Display(Name = "色谱柱")]
        [StringLength(1000)]
        public string Column { get; set; }

        [Display(Name = "真空度")]
        [StringLength(1000)]
        public string VacuumDegree { get; set; }

        [Display(Name = "BLANK信号")]
        [StringLength(1000)]
        public string Blank { get; set; }

        [Display(Name = "Test信号")]
        [StringLength(1000)]
        public string Test { get; set; }

        [Display(Name = "临床样品数量")]
        public int ClinicSampleNumber { get; set; }

        [Display(Name = "序列样品总数")]
        public int BatchSampleNumber { get; set; }

        [Display(Name = "结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "使用者")]
        [StringLength(10)]
        public string User { get; set; }

        [Display(Name = "备注")]
        [StringLength(1000)]
        public string Remark { get; set; }

        public void SetColumnInfo(List<Column> columns)
        {
            Column = JsonConvert.SerializeObject(columns);
        }

        public List<Column> GetColumnInfo()
        {
            return string.IsNullOrEmpty(Column) ? null : JsonConvert.DeserializeObject<List<Column>>(Column);
        }


        public void SetVacuumDegreeInfo(List<VacuumDegree> vacuums)
        {
            VacuumDegree = JsonConvert.SerializeObject(vacuums);
        }

        public List<VacuumDegree> GetVacuumDegreeInfo()
        {
            return string.IsNullOrEmpty(VacuumDegree) ? null : JsonConvert.DeserializeObject<List<VacuumDegree>>(VacuumDegree);
        }

        public void SetTestInfo(List<Test> tests)
        {
            Test = JsonConvert.SerializeObject(tests);
        }

        public List<Test> GetTestInfo()
        {
            return string.IsNullOrEmpty(Test) ? null : JsonConvert.DeserializeObject<List<Test>>(Test);
        }

        public void UpdateEndTime(DateTime endTime)
        {
            EndTime = endTime;
        }
    }
}
