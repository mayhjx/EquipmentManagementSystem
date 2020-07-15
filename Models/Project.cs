using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

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
        [StringLength(100)]
        public string ColumnType { get; set; }

        [Display(Name = "流动相/载气")]
        [StringLength(50)]
        public string Carrier { get; set; }

        [Display(Name = "离子源类型")]
        [StringLength(50)]
        public string IonSource { get; set; }

        [Display(Name = "检测器")]
        [StringLength(20)]
        public string Detector { get; set; }
    }
}
