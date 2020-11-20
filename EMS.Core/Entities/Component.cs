using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities
{
    public class Component : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "设备编号")]
        public string InstrumentNumber { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [Required]
        [Display(Name = "序列号")]
        [StringLength(100)]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "名称")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "型号")]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        [Display(Name = "品牌")]
        [StringLength(100)]
        public string Brand { get; set; }
    }
}
