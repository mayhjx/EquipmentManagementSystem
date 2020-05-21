using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 配件下单
    /// </summary>
    public class AccessoriesOrder
    {
        [Display(Name = "配件名称")]
        public string Name { get; set; }

        [Display(Name = "下单时间")]
        public DateTime PlaceTime { get; set; }

        [Display(Name = "到达时间")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
