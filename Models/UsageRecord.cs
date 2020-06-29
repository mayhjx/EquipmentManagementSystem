using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Models
{
    public class UsageRecord
    {
        public int Id { get; set; }

        [Display(Name = "创建时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "仪器编号")]
        public string InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [Display(Name = "项目名称")]
        [StringLength(50)]
        public string ProjectName { get; set; }
        public Project Project { get; set; }

        [Display(Name = "维护开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTimeOfMaintain { get; set; }

        [Display(Name = "色谱柱编号")]
        [StringLength(20)]
        public string ColumnNumber { get; set; }

        [Display(Name = "色谱柱压力")]
        public float? ColumnPressure { get; set; }

        [Display(Name = "色谱柱二编号")]
        [StringLength(20)]
        public string ColumnTwoNumber { get; set; }

        [Display(Name = "色谱柱二压力")]
        public float? ColumnTwoPressure { get; set; }

        [Display(Name = "压力单位")]
        [StringLength(10)]
        public string PressureUnit { get; set; }

        [Display(Name = "进样开始时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? BeginTimeOfTest { get; set; }

        [Display(Name = "临床样数量")]
        public int SampleNumber { get; set; }

        [Display(Name = "序列样品总数")]
        public int TestNumber { get; set; }

        [Display(Name = "进样结束时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "使用者")]
        [StringLength(10)]
        public string Creator { get; set; }

        [Display(Name = "备注")]
        [StringLength(999)]
        public string Remark { get; set; }

    }
}
