using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "项目名称")]
        public string Name { get; set; }

        [Display(Name = "所属项目组")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Display(Name = "单个样品检测时间")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: HH:mm:ss}")]
        public DateTime? SimpleTestTime { get; set; }

        [Display(Name = "色谱柱类型")]
        [StringLength(1000)]
        public string ColumnType { get; set; }

        [Display(Name = "色谱柱压力个数")]
        public int NumberOfColumn { get; set; }

        [Display(Name = "色谱柱压力单位")]
        [StringLength(10)]
        public string UnitOfColumn { get; set; }

        [Display(Name = "真空度个数")]
        public int NumberOfVacuumDegree { get; set; }

        [Display(Name = "真空度压力单位")]
        [StringLength(10)]
        public string UnitOfVacuumDegree { get; set; }

        [Display(Name = "Test个数")]
        public int NumberOfTest { get; set; }

        [Display(Name = "Test压力单位")]
        [StringLength(10)]
        public string UnitOfTest { get; set; }

        [Display(Name = "流动相/载气")]
        [StringLength(1000)]
        public string MobilePhase { get; set; }

        [Display(Name = "离子源")]
        [StringLength(100)]
        public string IonSource { get; set; }

        [Display(Name = "检测器")]
        [StringLength(100)]
        public string Detector { get; set; }
    }
}
