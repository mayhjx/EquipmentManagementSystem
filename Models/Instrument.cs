using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Models
{
    public class Instrument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="编号")]
        [RegularExpression(@"^[A-Z]+-[A-Z0-9]+$")]
        public string ID { get; set; }
        
        [Display(Name="创建时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        // SQLite 有bug
        [Display(Name="修改时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [Display(Name="平台")]
        [StringLength(50, MinimumLength=1)]
        public string Platform { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(50, MinimumLength=1)]
        public string Name { get; set; }
        
        [Display(Name="启用时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime StartUsingDate { get; set; }

        [Range(1,5)]
        [Display(Name="校准周期（年）")]
        public int CalibrationCycle { get; set; }

        [Display(Name="计量特性")]
        [StringLength(10,MinimumLength=1)]
        public string MetrologicalCharacteristics { get; set; }

        [Display(Name="状态")]
        public Status Status { get; set; }

        [Required]
        [Display(Name="位置")]
        [StringLength(50,MinimumLength=1)]
        public string Location { get; set; }

        [Required]      
        [Display(Name="负责人")]
        [StringLength(10,MinimumLength=1)]
        public string Principal { get; set; }

        [Display(Name="备注")]
        [StringLength(999)]
        public string Remark { get; set; }

        [Display(Name="新系统编号")]
        [StringLength(10)]
        public string NewSystemCode { get; set; }

        [Display(Name="设备电脑")]
        public Computer computer { get; set; }

        [Display(Name="资产信息")]
        public Assert assert { get; set; }

        [Display(Name="所属项目组")]
        public ProjectTeam projectTeam { get; set; }

        [Display(Name="检测项目")]
        public ICollection<Project> projects { get; set; }

        [Display(Name="校准信息")]
        public ICollection<Calibration> calibrations { get; set; }

        [Display(Name="部件")]
        public ICollection<Component> components { get; set; }


    }

    public enum Status {
        
        [Display(Name="使用中")]
        Using,
        [Display(Name="故障")]
        Malfunctoin,
        [Display(Name="报废")]
        Scrapped
    }
}