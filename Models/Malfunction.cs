using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Malfunction
    {
        public int ID { get; set; }

        // [Required]
        // [Display(Name="设备编号")]
        // public string instrumentID { get; set;}
        
        // public Instrument instrument { get; set; }

        [Display(Name="部件")]
        public int componentID { get; set; }

        public Component component { get; set; }

        [Required]
        [Display(Name="类别")]
        [StringLength(50,MinimumLength=1)]
        public string Category { get; set; }

        [Required]
        [Display(Name="现象/问题")]
        [StringLength(50,MinimumLength=1)]
        public string Problem { get; set; }

        [Required]
        [Display(Name="原因")]
        [StringLength(50,MinimumLength=1)]
        public string Reason { get; set; }

        [Display(Name="发现时间")]
        public DateTime FoundTime { get; set; }

        [Display(Name="排查开始时间")]
        public DateTime StartTrackTime { get; set; }

        [Display(Name="报修时间")]
        public DateTime ReportTime { get; set; }

        [Required]
        [Display(Name="跟进人")]
        [StringLength(10,MinimumLength=1)]
        public string FollowUpPeople { get; set; }

        [Display(Name="排除时间")]
        public DateTime DebuggingTime { get; set; }

        [Display(Name="配件下单时间")]      
        public DateTime PlaceOrderTime { get; set; }

        [Display(Name="配件到达时间")]      
        public DateTime AccessoriesArrivalTime { get; set; }

        [Display(Name="工程师上门时间")]      
        public DateTime EngineerArrivalTime { get; set; }

        [Display(Name="解决措施")]
        public string Solutions { get; set; }

        [Display(Name="备注")]
        public string Remark { get; set; }
        
    }
}