using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Component
    {
        public int ID { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [Required]
        [Display(Name = "序列号")]
        [StringLength(50, MinimumLength = 1)]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "名称")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "型号")]
        [StringLength(50, MinimumLength = 1)]
        public string Model { get; set; }

        [Required]
        [Display(Name = "品牌")]
        [StringLength(50, MinimumLength = 1)]
        public string Brand { get; set; }
    }
}
