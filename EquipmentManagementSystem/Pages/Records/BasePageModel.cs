using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EquipmentManagementSystem.Pages.Records
{
    public class BasePageModel : PageModel
    {
        protected EquipmentContext _context { get; }
        protected UserManager<User> _userManager { get; }
        protected IAuthorizationService _authorizationService { get; }

        public BasePageModel(
            EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public SelectList ProjectsSelectList { get; set; }

        //public void PopulateProjectDropDownList()
        //{
        //    var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);
        //    var userGroup = _userManager.GetUserAsync(User).Result?.Group;

        //    if (isAdmin || userGroup == null)
        //    {
        //        ProjectsSelectList = new SelectList(_context.Projects, "Name", "Name");
        //    }
        //    else
        //    {
        //        ProjectsSelectList = new SelectList(_context.Projects.Include(p => p.Group).Where(p => p.Group.Name == userGroup), "Name", "Name");
        //    }
        //}
    }
}
