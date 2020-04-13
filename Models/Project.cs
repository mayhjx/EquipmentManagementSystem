using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="名称")]
        [StringLength(50, MinimumLength=1)]
        public string Name { get; set; }

        [Display(Name="仪器编号")]
        public string instrumentID { get; set; }
        public Instrument instrument { get; set; }
            
        // 不需要Project Model？
        [Required]
        [Display(Name="项目组")]
        public string ProjectTeamName { get; set; }

        public ProjectTeam projectTeam { get; set; }
        
    }
}