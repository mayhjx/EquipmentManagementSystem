using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentManagementSystem.Models
{
    public class Instrument
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="编号")]
        [RegularExpression(@"^[A-Z]+-[A-Z0-9]+$")]
        public string ID { get; set; }

        [Display(Name="创建时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [Display(Name="修改时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [Display(Name="平台")]
        [StringLength(50, MinimumLength=1)]
        public string Platform { get; set; }

        [Required]
        [Display(Name="序列号")]
        [StringLength(50,MinimumLength=1)]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(50,MinimumLength=1)]
        public string Name { get; set; }

        [Required]
        [Display(Name="品牌")]
        [StringLength(50,MinimumLength=1)]
        public string Brand { get; set; }

        [Required]
        [Display(Name="型号")]
        [StringLength(50,MinimumLength=1)]
        public string Model { get; set; }
        
        [Display(Name="启用时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime StartUsingDate { get; set; }

        [Range(1,100)]
        [Display(Name="校准周期")]
        public int CalibrationCycle { get; set; }

        [Display(Name="状态")]
        public Status Status { get; set; }

        [Required]
        [Display(Name="位置")]
        [StringLength(50,MinimumLength=1)]
        public string Location { get; set; }

        [Display(Name="备注")]
        [StringLength(500)]
        public string Remark { get; set; }

        
        public Assert assert { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ProjectTeam projectTeam { get; set; }
        public ICollection<Calibration> calibrations { get; set; }
        public ICollection<Component> components { get; set; }

    }

    public enum Status {
        Using, // 使用中
        Malfunctoin, // 故障
        Scrapped // 报废
    }
}