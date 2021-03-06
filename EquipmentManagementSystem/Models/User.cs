using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class User : IdentityUser
    {
        [StringLength(10)]
        [Display(Name = "工号")]
        public string Number { get; set; }

        [StringLength(10)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "项目组")]
        public string Group { get; set; }
    }
}
