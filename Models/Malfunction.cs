using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Malfunction
    {
        public int ID { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        public Instrument Instrument { get; set; }


        [Display(Name = "部件")]
        public MalfunctionPart Part { get; set; }


        [Display(Name = "问题/现象")]
        public MalfunctionProblem MalfunctionProblem { get; set; }

        [Display(Name = "原因")]
        public MalfunctionReason MalfunctionReason { get; set; }

        [Display(Name = "开始时间")]
        [DataType(DataType.DateTime)]
        public DateTime BeginTime { get; set; }

        [Display(Name = "发现时间")]
        [DataType(DataType.DateTime)]
        public DateTime FoundedTime { get; set; }

        [Display(Name = "结束时间")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "排查记录")]
        public ICollection<MalfunctionInvestigation> Investigation { get; set; }

        [Display(Name = "报修时间")]
        public DateTime? ReportTime { get; set; }

        [Required]
        [Display(Name = "跟进人")]
        [StringLength(10, MinimumLength = 1)]
        public string FollowUpPeople { get; set; }

        [Display(Name = "配件下单时间")]
        public DateTime? PlaceOrderTime { get; set; }

        [Display(Name = "配件到达时间")]
        public DateTime? AccessoriesArrivalTime { get; set; }

        [Display(Name = "工程师上门时间")]
        public DateTime? EngineerArrivalTime { get; set; }

        [Display(Name = "解决措施")]
        public string Solutions { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "状态")]
        public MalfunctionStatus Status { get; set; }
    }

    public enum MalfunctionStatus
    {
        [Display(Name = "跟进中")]
        FollowUp,
        [Display(Name = "已完成")]
        Finished
    }

}