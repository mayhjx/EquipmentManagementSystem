using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EquipmentManagementSystem.Models
{
    public class Instrument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "编号")]
        [Required]
        [RegularExpression(@"^[A-Z]+-[A-Z]+[0-9]+$", ErrorMessage = "请按“字母-字母数字”（例：FXS-YZ01）的格式输入设备编号")]
        public string ID { get; set; }

        [Required]
        [Display(Name = "平台")]
        public string Platform { get; set; }

        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "启用日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartUsingDate { get; set; }

        [Range(1, 100)]
        [Display(Name = "校准周期（年）")]
        public int CalibrationCycle { get; set; }

        [Display(Name = "计量特性")]
        [StringLength(10, MinimumLength = 1)]
        public string MetrologicalCharacteristics { get; set; }

        [Display(Name = "状态")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "存放位置")]
        [StringLength(50, MinimumLength = 1)]
        public string Location { get; set; }

        [Required]
        [Display(Name = "负责人")]
        [StringLength(10, MinimumLength = 1)]
        public string Principal { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "新系统编号")]
        [StringLength(10)]
        public string NewSystemCode { get; set; }

        [Display(Name = "设备电脑")]
        public Computer Computer { get; set; }

        [Display(Name = "资产信息")]
        public Assert Assert { get; set; }

        [Display(Name = "所属项目组")]
        public string Group { get; set; }

        [Display(Name = "检测项目")]
        public string Projects { get; set; }

        public List<string> GetProjects()
        {
            return Projects?.Split(", ").ToList() ?? new List<string>();
        }

        public void SetProjects(List<string> projects)
        {
            Projects = string.Join(", ", projects);
        }

        public bool HasProject(string project)
        {
            return Projects?.Contains(project) ?? false;
        }

        [Display(Name = "校准信息")]
        public ICollection<Calibration> Calibrations { get; set; }

        [Display(Name = "部件信息")]
        public ICollection<Component> Components { get; set; }

        [Display(Name = "故障信息")]
        public ICollection<MalfunctionWorkOrder> MalfunctionWorkOrder { get; set; }
    }

    public enum InstrumentStatus
    {

        [Display(Name = "正常")]
        Using,
        [Display(Name = "故障")]
        Malfunction,
        [Display(Name = "停用")]
        StopUsing,
        [Display(Name = "调拨")]
        Allocate,
        [Display(Name = "报废")]
        Scrapped
    }
}
