using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Malfunction
    {
        public int ID { get; set; }

        [Display(Name="设备编号")]
        public string InstrumentID { get; set;}
        
        public Instrument instrument { get; set; }

        [Display(Name="部件")]
        public int ComponentID { get; set; }

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

        [Required]
        [Display(Name="发现时间")]
        public string FoundTime { get; set; }

        [Display(Name="排查开始时间")]
        public string StartTrackTime { get; set; }

        [Display(Name="报修时间")]
        public string ReportTime { get; set; }

        [Required]
        [Display(Name="跟进人")]
        [StringLength(10,MinimumLength=1)]
        public string FollowUpPeople { get; set; }

        [Display(Name="排除时间")]
        public string DebuggingTime { get; set; }

        [Display(Name="配件下单时间")]      
        public string PlaceOrderTime { get; set; }

        [Display(Name="配件到达时间")]      
        public string AccessoriesArrivalTime { get; set; }

        [Display(Name="工程师上门时间")]      
        public string EngineerArrivalTime { get; set; }

        [Display(Name="解决措施")]
        public string Solutions { get; set; }

        [Display(Name="备注")]
        public string Remark { get; set; }
        
    }
}