using EMS.Core.Entities.AcceptanceAggregate;
using EMS.Core.Entities.CalibrationAggregate;
using EMS.Core.Entities.GroupAggregate;
using EMS.Core.Entities.UsageRecordAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities
{
    public class Instrument : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "编号")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "平台")]
        [StringLength(50)]
        public string Platform { get; set; }

        [Required]
        [Display(Name = "名称")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "启用日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartUsingDate { get; set; }

        [Range(1, 100)]
        [Display(Name = "校准周期（年）")]
        public int CalibrationCycle { get; set; } = 1;

        [Required]
        [Display(Name = "计量特性")]
        [StringLength(10)]
        public string MetrologicalCharacteristics { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "状态")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "存放位置")]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "负责人")]
        public string PersonInCharge { get; set; }

        [StringLength(20)]
        [Display(Name = "新系统编号")]
        public string NewSystemCode { get; set; }

        [StringLength(1000)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "所属项目组")]
        public Group Group { get; set; }

        [Display(Name = "检测项目")]
        public IList<Project> Projects { get; set; }

        //[Display(Name = "设备电脑")]
        //public Computer Computer { get; set; }

        //[Display(Name = "资产信息")]
        //public Assert Assert { get; set; }

        [Display(Name = "验收信息")]
        public Acceptance InstrumentAssecptance { get; set; }

        [Display(Name = "使用信息")]
        public ICollection<UsageRecord> UsageRecords { get; set; }

        [Display(Name = "校准信息")]
        public ICollection<Calibration> Calibrations { get; set; }

        [Display(Name = "部件信息")]
        public ICollection<Component> Components { get; set; }

        // TODO
        //[Display(Name = "故障信息")]
        //public ICollection<Malfunction> MalfunctionWorkOrder { get; set; }

        //[Display(Name = "报废信息")]
        //public Obsolescence Obsolescence { get; set; }

        public void ChangeStatus(string status)
        {
            Status = status;
        }
    }
}
