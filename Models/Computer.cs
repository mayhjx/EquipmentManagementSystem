using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Computer
    {
        public int ID { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        [Display(Name = "账号")]
        [StringLength(50)]
        public string Account { get; set; }

        [Display(Name = "密码")]
        [StringLength(50)]
        public string Password { get; set; }
    }
}