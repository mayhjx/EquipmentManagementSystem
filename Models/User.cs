using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    public class User : IdentityUser
    {
        [StringLength(256)]
        [Display(Name = "工号")]
        public string Number { get; set; }

        [StringLength(256)]
        [Display(Name = "姓名")]
        public string Name { get; set; }
    }
}
